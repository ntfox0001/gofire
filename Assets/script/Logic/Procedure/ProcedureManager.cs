using System.Collections;
using GoFire.Kernel;

namespace GoFire
{
    public class ProcedureManager : Singleton<ProcedureManager>, IManager 
    {
        public IProcedure CurrentProcedure {get; private set;}

        public bool InSwitch { get; private set;}
        public IEnumerator Switch(IProcedure procedure, params object[] args)
        {
            if (InSwitch)
            {
                Log.Error("procedure is switching.");
                yield break;
            }
            InSwitch = true;
            if (CurrentProcedure != null)
            {
                yield return CurrentProcedure.Release();
            }
            CurrentProcedure = procedure;
            yield return CurrentProcedure.Init(args);
            InSwitch = false;
        }

        public IEnumerator Init()
        {
            yield return null;
        }

        public void Update()
        {
            
        }

        public void Release()
        {
            InSwitch = true;

            while (true)
            {
                if (CurrentProcedure == null)
                {
                    break;
                }
                
                var wait = CurrentProcedure.Release();
                if (!wait.MoveNext())
                {
                    break;
                }
            }
            
            CurrentProcedure = null;
        }
    }
}