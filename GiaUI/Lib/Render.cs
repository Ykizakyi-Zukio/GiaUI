using Cysharp.Text;
using GiaUI.Data;
using System.Diagnostics;
using System.Text;

public class Animation
{
    public bool IsPlaying { get; private set; } = false;
    public int WaitFromMs;
    public int PosX, PosY;
    public IDecorator[] DecoratorGroup;
    public string Separator = "\r\n";
    public string Text { get; set; } = string.Empty;

    private CancellationTokenSource? cts;
    private readonly object consoleLock = new();
    private readonly StringBuilder builder;

    public Animation(int waitFromMs, IDecorator[] dec, int posX = 0, int posY = 0)
    {
        PosX = posX;
        PosY = posY;
        WaitFromMs = waitFromMs;
        DecoratorGroup = dec ?? throw new ArgumentNullException(nameof(dec));

        if (DecoratorGroup.Length == 0)
            throw new ArgumentException("Decorator group cannot be empty.", nameof(dec));

        if (!DecoratorGroup[0].CanAnimate)
            throw new InvalidOperationException("This decorator can't be animated");

        DecoratorGroup[0].Phase = 0D;
        builder = new(2096);
    }

    public static Animation Single(int waitFromMs, IDecorator dec, int posX = 0, int posY = 0)
    {
        return new Animation(waitFromMs, new[] { dec }, posX, posY);
    }

    public void Start()
    {
        if (IsPlaying) return;
        Console.CursorVisible = false;

        cts = new CancellationTokenSource();
        var ct = cts.Token;

        for (int i = 0; i < DecoratorGroup.Length; i++)
        {
            DecoratorGroup[i].Phase = 0D;
        }

        IsPlaying = true;

        _ = Task.Run(async () =>
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    builder.Clear();

                    for (int i = 0; i < DecoratorGroup.Length; i++)
                    {
                        var dec = DecoratorGroup[i];
                        builder.Append(dec.Decorate());
                        if (i < DecoratorGroup.Length - 1)
                        {
                            builder.Append(Separator);
                        }
                        dec.Phase += 0.1D;
                    }

                    lock (consoleLock)
                    {
                        if (!ct.IsCancellationRequested)
                        {
                            Console.SetCursorPosition(PosX, PosY);
                            Console.Out.Write(builder.ToString());
                        }
                    }

                    await Task.Delay(WaitFromMs, ct);
                }
            }
            catch (OperationCanceledException) { }
            finally
            {
                IsPlaying = false;
            }
        }, ct);
    }

    public void Stop()
    {
        if (!IsPlaying) return;

        cts?.Cancel();
        cts?.Dispose();
        cts = null;

        lock (consoleLock)
        {
            Console.SetCursorPosition(0, Math.Min(Console.CursorTop + DecoratorGroup.Length, Console.BufferHeight - 1));
            Console.CursorVisible = true;
        }

        IsPlaying = false;
    }
}