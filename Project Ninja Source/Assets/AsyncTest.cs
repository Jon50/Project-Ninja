using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using static UnityEngine.Debug;

public class AsyncTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _debugText;
    private bool myBool = true;

    private static CancellationTokenSource _source = new CancellationTokenSource();
    private CancellationToken _token = _source.Token;

    private async Task Start()
    {
        _debugText.SetText(myBool.ToString());

        var t1 = new Thread(async () =>
        {
            float timer = 0f;

            while(myBool)
            {
                try
                {
                    await Task.Delay(500, _token);
                    timer += 0.1f;
                    timer.Dump();
                    if(timer > 2f)
                        myBool = false;
                }
                catch(Exception e)
                {
                    e.Dump();
                    throw e;
                }
            }
        });

        t1.Start();
        await Async.WaitUntil(() => myBool == false, 50, _token);

        _debugText.SetText(myBool.ToString());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
            _source.Cancel();
    }
}

public static class Async
{
    public static async Task WaitUntil( Func<bool> predicate, int delay = 50, CancellationToken token = default )
    {
        await Task.Run(async () =>
        {
            while(!predicate.Invoke())
            {
                "Waiting".Dump();
                await Task.Delay(delay, token);
            }
        }, token);
    }
}

public static class StringExtension
{
    public static void Dump( this object value ) => Debug.Log(value);
    public static string ToString( this object value, bool dump = false )
    {
        if(dump)
            value.Dump();
        return value.ToString();
    }
}