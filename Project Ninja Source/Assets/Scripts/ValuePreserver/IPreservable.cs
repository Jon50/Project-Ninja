using System.Collections.Generic;

namespace TGM.FutureRacingGP.Preservable
{
    public interface IPreservable
    {
        void RegisterPreservable();
        (string, object) PreserveValue();
        void SetPreservedValue(Dictionary<string, object> keyValuePair);
    }
}