using GiaUI.Data;
using System;
using System.Text;

public class Animation
{
    public bool IsPlaying = false;
    public int WaitFromMs;
    public int PosX, PosY;
    public IDecorator[] DecoratorGroup;
    public string Separator = "\r\n";
    public string Text { get; set; } = string.Empty;

    private CancellationTokenSource? cts;
    private CancellationToken ct;
    private readonly object consoleLock = new();


    public Animation(int waitFromMs, IDecorator[] dec, 
        int posX = 0, int posY = 0)
    {
        PosX = posX; PosY = posY;
        WaitFromMs = waitFromMs;
        DecoratorGroup = dec;

        if (DecoratorGroup[0].CanAnimate)
            DecoratorGroup[0].Phase = 0D;
        else
            throw new Exception("This decorator can't be animated");
    }

    public Animation Single(int waitFromMs, IDecorator dec,
        int posX = 0, int posY = 0)
    {
        return new Animation(waitFromMs, [dec], posX, posY);
    }

    public void Start()
    {
        if (IsPlaying) return;
        Console.CursorVisible = false;

        cts = new();
        ct = cts.Token;

        foreach (var dec in DecoratorGroup)
            dec.Phase = 0D;

        string output = "";
        
        IsPlaying = true;
        StringBuilder builder = new();
        

        _ = Task.Run(async () =>
        {
            try
            {
                while (IsPlaying && !ct.IsCancellationRequested)
                {
                    foreach (var dec in DecoratorGroup)
                    {
                        builder.Append(dec.Decorate() + Separator);
                        dec.Phase += 0.1D;
                    }

                    output = builder.ToString();

                    lock (consoleLock)
                    {
                        if (!ct.IsCancellationRequested)
                        {
                            Console.SetCursorPosition(PosX, PosY);
                            Console.Write(output);
                        }
                    }

                    await Task.Delay(WaitFromMs, ct);
                }
            }
            catch (TaskCanceledException) { }
            finally
            {
                IsPlaying = false;
            }
        }, ct);
    }

    public void Stop()
    {
        if(!IsPlaying) return;

        cts?.Cancel();
        cts?.Dispose();
        cts = null;

        lock (consoleLock)
        {
            int lastRow = Console.BufferHeight - 1;
            Console.SetCursorPosition(0, lastRow);
            Console.CursorVisible = true;
        }

        IsPlaying = false;
    }
}