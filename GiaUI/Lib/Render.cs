using GiaUI.Data;
using System;
using System.Text;

public class Renderer
{
    protected Double phase = 0D;

    public RGB? BaseColor { get; set; }
    public string? Text { get; set; }

    public async Task<string> RainbowAsync(StringBuilder buffer, double sinAngle = 0.2D, byte upper = 2)
    {
        if (string.IsNullOrEmpty(Text)) throw new Exception("Set text");
        buffer.Clear();

        for (int i = 0; i < Text.Length; i++)
        {
            int r = (int)(Math.Sin(sinAngle * i + phase + 0) * 127 + 128);
            int g = (int)(Math.Sin(sinAngle * i + phase + upper) * 127 + 128);
            int b = (int)(Math.Sin(sinAngle * i + phase + upper * 2) * 127 + 128);

            buffer.Append($"\x1b[38;2;{r};{g};{b}m{Text[i]}");
        }

        buffer.Append("\x1b[0m");
        phase += 0.1;
        return buffer.ToString();
    }

    public async Task<string> ShadeAsync(StringBuilder buffer)
    {
        if (string.IsNullOrEmpty(Text)) throw new Exception("Set text");
        buffer.Clear();

        if (BaseColor == null)
            throw new Exception("Please choose base color");

        //var sb = new StringBuilder();
        for (int i = 0; i < Text.Length; i++)
        {
            double brightness = Math.Cos(0.2 * i + phase) * 0.4 + 0.6;

            int r = (int)(BaseColor?.R* brightness);
            int g = (int)(BaseColor?.G * brightness);
            int b = (int)(BaseColor?.B * brightness);

            buffer.Append($"\x1b[38;2;{r};{g};{b}m{Text[i]}");
        }
        buffer.Append("\x1b[0m");
        phase += 0.1D;
        return buffer.ToString();
    }
}

public class Animation : Renderer
{
    public bool IsPlaying = false;
    public int WaitFromMs;
    public int PosX, PosY;
    public AnimationType AnimationType;

    private CancellationTokenSource cts;
    private CancellationToken ct;
    private readonly object consoleLock = new();
    StringBuilder sb = new();


    public Animation(int waitfromms, AnimationType animationType) 
    {
        AnimationType = animationType;
        PosX = Console.CursorLeft;
        PosY = Console.CursorTop;
        WaitFromMs = waitfromms;
        cts = new CancellationTokenSource();
        ct = cts.Token;
    }
    public Animation(int waitfromms, AnimationType animationType, int posX, int posY)
    {
        AnimationType = animationType;
        PosX = posX; PosY = posY;
        WaitFromMs = waitfromms;
        cts = new CancellationTokenSource();
        ct = cts.Token;
    }

    public void Start()
    {
        if (IsPlaying) return;
        Console.CursorVisible = false;

        cts = new();
        ct = cts.Token;

        string output;
        phase = 0;
        IsPlaying = true;
        

        _ = Task.Run(async () =>
        {
            try
            {
                while (IsPlaying && !ct.IsCancellationRequested)
                {
                    string output = AnimationType switch
                    {
                        AnimationType.Rainbow => await RainbowAsync(sb),
                        AnimationType.Shade => await ShadeAsync(sb),
                        _ => string.Empty
                    };

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

public enum AnimationType : byte
{
    Rainbow,
    Shade
}