using Il2Cpp;
using Il2CppGoyfs.Context;
using Il2CppGoyfs.Instance;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes;
using Goyfs = Il2CppGoyfs;

namespace BhapticsPopOne.Utils
{
    public class GoyfsHelper
    {
        public static Context DefaultContext => SceneContext.Instance;

        #region HasBinding

        public static bool HasBinding<T>() where T : Il2CppObjectBase
        {
            return HasBinding<T>(DefaultContext?.InstanceBinder);
        }
        
        public static bool HasBinding<T>(IInstanceBinder binder) where T : Il2CppObjectBase
        {
            var b = binder.Cast<InstanceBinder>();
            return b.bindings.ContainsKey(Il2CppType.Of<T>());
        }

        #endregion
        
        #region Get

        public static bool TryGet<T>(IInstanceBinder binder, out T result) where T : Il2CppObjectBase
        {
            if (binder == null)
            {
                result = default(T);
                return false;
            }
            
            var b = binder.Cast<InstanceBinder>();
            if (!HasBinding<T>(binder))
            {
                result = default(T);
                return false;
            }

            result = b.bindings[Il2CppType.Of<T>()].GetInstance().Cast<T>();
            return true;
        }
        
        public static bool TryGet<T>(out T result) where T : Il2CppObjectBase
        {
            return TryGet(DefaultContext?.InstanceBinder, out result);
        }
        
        public static T Get<T>(IInstanceBinder binder) where T : Il2CppObjectBase
        {
            T res;
            if (!TryGet(binder, out res))
                return default(T);

            return res;
        }
        
        public static T Get<T>() where T : Il2CppObjectBase
        {
            return Get<T>(DefaultContext?.InstanceBinder);
        }

        #endregion

        #region AddListener

        public static void AddListener<TSignal>(System.Action action) where TSignal : Goyfs.Signal.Signal
        {
            AddListener<TSignal>(DefaultContext?.InstanceBinder, action);
        }
        
        public static void AddListener<TSignal>(IInstanceBinder binder, System.Action action) where TSignal : Goyfs.Signal.Signal
        {
            TryAddListener<TSignal>(binder, action);
        }
        
        public static void AddListener<TSignal, TArg1>(System.Action<TArg1> action) where TSignal : Goyfs.Signal.Signal<TArg1>
        {
            AddListener<TSignal, TArg1>(DefaultContext?.InstanceBinder, action);

        }
        
        public static void AddListener<TSignal, TArg1>(IInstanceBinder binder, System.Action<TArg1> action) where TSignal : Goyfs.Signal.Signal<TArg1>
        {
            TryAddListener<TSignal, TArg1>(binder, action);

        }
        
        public static void AddListener<TSignal, TArg1, TArg2>(System.Action<TArg1, TArg2> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2>
        {
            AddListener<TSignal, TArg1, TArg2>(DefaultContext?.InstanceBinder, action);
        }
        
        public static void AddListener<TSignal, TArg1, TArg2>(IInstanceBinder binder, System.Action<TArg1, TArg2> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2>
        {
            TryAddListener<TSignal, TArg1, TArg2>(binder, action);
        }
        
        public static void AddListener<TSignal, TArg1, TArg2, TArg3>(System.Action<TArg1, TArg2, TArg3> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3>
        {
            AddListener<TSignal, TArg1, TArg2, TArg3>(DefaultContext?.InstanceBinder, action);
        }
        
        public static void AddListener<TSignal, TArg1, TArg2, TArg3>(IInstanceBinder binder, System.Action<TArg1, TArg2, TArg3> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3>
        {
            TryAddListener<TSignal, TArg1, TArg2, TArg3>(binder, action);
        }

        public static void AddListener<TSignal, TArg1, TArg2, TArg3, TArg4>(System.Action<TArg1, TArg2, TArg3, TArg4> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3, TArg4>
        {
            AddListener<TSignal, TArg1, TArg2, TArg3, TArg4>(DefaultContext?.InstanceBinder, action);
        }
        
        public static void AddListener<TSignal, TArg1, TArg2, TArg3, TArg4>(IInstanceBinder binder, System.Action<TArg1, TArg2, TArg3, TArg4> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3, TArg4>
        {
            TryAddListener<TSignal, TArg1, TArg2, TArg3, TArg4>(binder, action);

        }

        #endregion
        
        #region TryAddListener

        public static bool TryAddListener<TSignal>(System.Action action) where TSignal : Goyfs.Signal.Signal
        {
            return TryAddListener<TSignal>(DefaultContext?.InstanceBinder, action);
        }
        
        public static bool TryAddListener<TSignal>(IInstanceBinder binder, System.Action action) where TSignal : Goyfs.Signal.Signal
        {
            if (binder == null)
                return false;
            
            TSignal signal;
            if (!TryGet(binder, out signal))
                return false;

            signal.AddListener(ConvertAction(action));
            return true;
        }
        
        public static bool TryAddListener<TSignal, TArg1>(System.Action<TArg1> action) where TSignal : Goyfs.Signal.Signal<TArg1>
        {
            return TryAddListener<TSignal, TArg1>(DefaultContext?.InstanceBinder, action);
        }
        
        public static bool TryAddListener<TSignal, TArg1>(IInstanceBinder binder, System.Action<TArg1> action) where TSignal : Goyfs.Signal.Signal<TArg1>
        {
            if (binder == null)
                return false;
            
            TSignal signal;
            if (!TryGet(binder, out signal))
                return false;

            signal.AddListener(ConvertAction(action));
            return true;
        }
        
        public static bool TryAddListener<TSignal, TArg1, TArg2>(System.Action<TArg1, TArg2> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2>
        {
            return TryAddListener<TSignal, TArg1, TArg2>(DefaultContext?.InstanceBinder, action);
        }
        
        public static bool TryAddListener<TSignal, TArg1, TArg2>(IInstanceBinder binder, System.Action<TArg1, TArg2> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2>
        {
            if (binder == null)
                return false;
            
            TSignal signal;
            if (!TryGet(binder, out signal))
                return false;

            signal.AddListener(ConvertAction(action));
            return true;
        }
        
        public static bool TryAddListener<TSignal, TArg1, TArg2, TArg3>(System.Action<TArg1, TArg2, TArg3> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3>
        {
            return TryAddListener<TSignal, TArg1, TArg2, TArg3>(DefaultContext?.InstanceBinder, action);
        }
        
        public static bool TryAddListener<TSignal, TArg1, TArg2, TArg3>(IInstanceBinder binder, System.Action<TArg1, TArg2, TArg3> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3>
        {
            if (binder == null)
                return false;
            
            TSignal signal;
            if (!TryGet(binder, out signal))
                return false;

            signal.AddListener(ConvertAction(action));
            return true;
        }

        public static bool TryAddListener<TSignal, TArg1, TArg2, TArg3, TArg4>(System.Action<TArg1, TArg2, TArg3, TArg4> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3, TArg4>
        {
            return TryAddListener<TSignal, TArg1, TArg2, TArg3, TArg4>(DefaultContext?.InstanceBinder, action);
        }
        
        public static bool TryAddListener<TSignal, TArg1, TArg2, TArg3, TArg4>(IInstanceBinder binder, System.Action<TArg1, TArg2, TArg3, TArg4> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3, TArg4>
        {
            if (binder == null)
                return false;
            
            TSignal signal;
            if (!TryGet(binder, out signal))
                return false;

            signal.AddListener(ConvertAction(action));
            return true;
        }
        
        #endregion
        
        #region RemoveListener

        public static void RemoveListener<TSignal>(System.Action action) where TSignal : Goyfs.Signal.Signal
        {
            RemoveListener<TSignal>(DefaultContext?.InstanceBinder, action);
        }
        
        public static void RemoveListener<TSignal>(IInstanceBinder binder, System.Action action) where TSignal : Goyfs.Signal.Signal
        {
            TryRemoveListener<TSignal>(binder, action);
        }
        
        public static void RemoveListener<TSignal, TArg1>(System.Action<TArg1> action) where TSignal : Goyfs.Signal.Signal<TArg1>
        {
            RemoveListener<TSignal, TArg1>(DefaultContext?.InstanceBinder, action);
        }
        
        public static void RemoveListener<TSignal, TArg1>(IInstanceBinder binder, System.Action<TArg1> action) where TSignal : Goyfs.Signal.Signal<TArg1>
        {
            TryRemoveListener<TSignal, TArg1>(binder, action);
        }
        
        public static void RemoveListener<TSignal, TArg1, TArg2>(System.Action<TArg1, TArg2> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2>
        {
            RemoveListener<TSignal, TArg1, TArg2>(DefaultContext?.InstanceBinder, action);
        }
        
        public static void RemoveListener<TSignal, TArg1, TArg2>(IInstanceBinder binder, System.Action<TArg1, TArg2> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2>
        {
            TryRemoveListener<TSignal, TArg1, TArg2>(binder, action);
        }
        
        public static void RemoveListener<TSignal, TArg1, TArg2, TArg3>(System.Action<TArg1, TArg2, TArg3> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3>
        {
            RemoveListener<TSignal, TArg1, TArg2, TArg3>(DefaultContext?.InstanceBinder, action);
        }
        
        public static void RemoveListener<TSignal, TArg1, TArg2, TArg3>(IInstanceBinder binder, System.Action<TArg1, TArg2, TArg3> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3>
        {
            TryRemoveListener<TSignal, TArg1, TArg2, TArg3>(binder, action);
        }

        public static void RemoveListener<TSignal, TArg1, TArg2, TArg3, TArg4>(System.Action<TArg1, TArg2, TArg3, TArg4> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3, TArg4>
        {
            RemoveListener<TSignal, TArg1, TArg2, TArg3, TArg4>(DefaultContext?.InstanceBinder, action);
        }
        
        public static void RemoveListener<TSignal, TArg1, TArg2, TArg3, TArg4>(IInstanceBinder binder, System.Action<TArg1, TArg2, TArg3, TArg4> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3, TArg4>
        {
            TryRemoveListener<TSignal, TArg1, TArg2, TArg3, TArg4>(binder, action);
        }

        #endregion
        
        #region TryRemoveListener

        public static bool TryRemoveListener<TSignal>(System.Action action) where TSignal : Goyfs.Signal.Signal
        {
            return TryRemoveListener<TSignal>(DefaultContext?.InstanceBinder, action);
        }
        
        public static bool TryRemoveListener<TSignal>(IInstanceBinder binder, System.Action action) where TSignal : Goyfs.Signal.Signal
        {
            if (binder == null)
                return false;
            
            TSignal signal;
            if (!TryGet(binder, out signal))
                return false;

            signal.RemoveListener(ConvertAction(action));
            return true;
        }
        
        public static bool TryRemoveListener<TSignal, TArg1>(System.Action<TArg1> action) where TSignal : Goyfs.Signal.Signal<TArg1>
        {
            return TryRemoveListener<TSignal, TArg1>(DefaultContext?.InstanceBinder, action);
        }
        
        public static bool TryRemoveListener<TSignal, TArg1>(IInstanceBinder binder, System.Action<TArg1> action) where TSignal : Goyfs.Signal.Signal<TArg1>
        {
            if (binder == null)
                return false;
            
            TSignal signal;
            if (!TryGet(binder, out signal))
                return false;

            signal.RemoveListener(ConvertAction(action));
            return true;
        }
        
        public static bool TryRemoveListener<TSignal, TArg1, TArg2>(System.Action<TArg1, TArg2> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2>
        {
            return TryRemoveListener<TSignal, TArg1, TArg2>(DefaultContext?.InstanceBinder, action);
        }
        
        public static bool TryRemoveListener<TSignal, TArg1, TArg2>(IInstanceBinder binder, System.Action<TArg1, TArg2> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2>
        {
            if (binder == null)
                return false;
            
            TSignal signal;
            if (!TryGet(binder, out signal))
                return false;

            signal.RemoveListener(ConvertAction(action));
            return true;
        }
        
        public static bool TryRemoveListener<TSignal, TArg1, TArg2, TArg3>(System.Action<TArg1, TArg2, TArg3> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3>
        {
            return TryRemoveListener<TSignal, TArg1, TArg2, TArg3>(DefaultContext?.InstanceBinder, action);
        }
        
        public static bool TryRemoveListener<TSignal, TArg1, TArg2, TArg3>(IInstanceBinder binder, System.Action<TArg1, TArg2, TArg3> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3>
        {
            if (binder == null)
                return false;
            
            TSignal signal;
            if (!TryGet(binder, out signal))
                return false;

            signal.RemoveListener(ConvertAction(action));
            return true;
        }

        public static bool TryRemoveListener<TSignal, TArg1, TArg2, TArg3, TArg4>(System.Action<TArg1, TArg2, TArg3, TArg4> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3, TArg4>
        {
            return TryRemoveListener<TSignal, TArg1, TArg2, TArg3, TArg4>(DefaultContext?.InstanceBinder, action);
        }
        
        public static bool TryRemoveListener<TSignal, TArg1, TArg2, TArg3, TArg4>(IInstanceBinder binder, System.Action<TArg1, TArg2, TArg3, TArg4> action) where TSignal : Goyfs.Signal.Signal<TArg1, TArg2, TArg3, TArg4>
        {
            if (binder == null)
                return false;
            
            TSignal signal;
            if (!TryGet(binder, out signal))
                return false;

            signal.RemoveListener(ConvertAction(action));
            return true;
        }
        
        #endregion

        #region ConvertAction
        
        public static Il2CppSystem.Action ConvertAction(System.Action action)
        {
            return DelegateSupport.ConvertDelegate<Il2CppSystem.Action>(action);
        }
        
        public static Il2CppSystem.Action<TArg1> ConvertAction<TArg1>(System.Action<TArg1> action)
        {
            return DelegateSupport.ConvertDelegate<Il2CppSystem.Action<TArg1>>(action);
        }
        
        public static Il2CppSystem.Action<TArg1, TArg2> ConvertAction<TArg1, TArg2>(System.Action<TArg1, TArg2> action)
        {
            return DelegateSupport.ConvertDelegate<Il2CppSystem.Action<TArg1, TArg2>>(action);
        }
        
        public static Il2CppSystem.Action<TArg1, TArg2, TArg3> ConvertAction<TArg1, TArg2, TArg3>(System.Action<TArg1, TArg2, TArg3> action)
        {
            return DelegateSupport.ConvertDelegate<Il2CppSystem.Action<TArg1, TArg2, TArg3>>(action);
        }
        
        public static Il2CppSystem.Action<TArg1, TArg2, TArg3, TArg4> ConvertAction<TArg1, TArg2, TArg3, TArg4>(System.Action<TArg1, TArg2, TArg3, TArg4> action)
        {
            return DelegateSupport.ConvertDelegate<Il2CppSystem.Action<TArg1, TArg2, TArg3, TArg4>>(action);
        }
        
        #endregion
    }
}