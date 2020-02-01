//Messenger.cs v1.0 by Artyom Isrgakov, wwwartty@gmail.com
//
// Based on Julie Iaccarino & Magnus Wolffelt's Messenger:
// http://wiki.unity3d.com/index.php/CSharpMessenger_Extended
//
// This is a C# messenger (notification center). It uses delegates
// and generics to provide type-checked messaging between event producers and
// event consumers, without the need for producers or consumers to be aware of
// each other. The major improvement from Hyde's implementation is that
// there is more extensive error detection, preventing silent bugs.
//
// Usage example:
// public enum MyEvents{ MyFirstEvent, MySecondEvent}
// public enum AnotherEvents { MyFirstEvent, MySecondEvent} 
// ...
// Messenger<float>.AddListener(MyEvents.MyFirstEvent, MyEventHandler);
// ...
// Messenger<float>.Broadcast(MyEvents.MyFirstEvent, 1.0f);
//
// Callback example:
// Messenger<float>.AddListener<string>(AnotherEvents.MyFirstEvent, MyEventHandler);
// private string MyEventHandler(float f1) { return "Test " + f1; }
// ...
// Messenger<float>.Broadcast<string>(AnotherEvents.MyFirstEvent, 1.0f, MyEventCallback);
// private void MyEventCallback(string s1) { Debug.Log(s1"); }
//
// If preferred, change DEFAULT_MODE to not require listeners.

using System;
using System.Collections.Generic;
using System.Linq;

public enum MessengerMode
{
    DONT_REQUIRE_LISTENER,
    REQUIRE_LISTENER,
}

    static internal class MessengerInternal
    {
        readonly public static Dictionary<Type,Dictionary<Enum, Delegate>> eventTable = new Dictionary<Type, Dictionary<Enum, Delegate>>();
        static public MessengerMode DEFAULT_MODE = MessengerMode.REQUIRE_LISTENER;

        static public void AddListener(Enum enumEvent, Delegate callback)
        {
            Type t = enumEvent.GetType();

            MessengerInternal.OnListenerAdding(t,enumEvent, callback);
            eventTable[t][enumEvent] = Delegate.Combine(eventTable[t][enumEvent], callback);
        }

        static public void RemoveListener(Enum enumEvent, Delegate handler)
        {
            Type t = enumEvent.GetType();

            MessengerInternal.OnListenerRemoving(t, enumEvent, handler);
            eventTable[t][enumEvent] = Delegate.Remove(eventTable[t][enumEvent], handler);
            MessengerInternal.OnListenerRemoved(t,enumEvent);
        }

        static public T[] GetInvocationList<T>(Enum enumEvent)
        {
            Type t = enumEvent.GetType();
#pragma warning disable IDE0018 // Объявление встроенной переменной
        Delegate d;
#pragma warning restore IDE0018 // Объявление встроенной переменной

        if (eventTable.ContainsKey(t))
            {
                if (eventTable[t].TryGetValue(enumEvent, out d))
                {
                    try
                    {
                        return d.GetInvocationList().Cast<T>().ToArray();
                    }
                    catch
                    {
                        throw MessengerInternal.CreateBroadcastSignatureException(t.ToString()+"."+enumEvent.ToString());
                    }
                }
            }
            return new T[0];
        }

        static public void OnListenerAdding(Type t, Enum eventEnum, Delegate listenerBeingAdded)
        {
            if (!eventTable.ContainsKey(t))
            {
                eventTable.Add(t, new Dictionary<Enum, Delegate>());
            }

            if (!eventTable[t].ContainsKey(eventEnum))
            {
                eventTable[t].Add(eventEnum, null);
            }

            var d = eventTable[t][eventEnum];
            if (d != null && d.GetType() != listenerBeingAdded.GetType())
            {
                throw new ListenerException(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eventEnum, d.GetType().Name, listenerBeingAdded.GetType().Name));
            }
        }

        static public void OnListenerRemoving(Type t, Enum eventEnum, Delegate listenerBeingRemoved)
        {
            if (eventTable.ContainsKey(t))
            {

                if (eventTable[t].ContainsKey(eventEnum))
                {
                    var d = eventTable[t][eventEnum];

                    if (d == null)
                    {
                        throw new ListenerException(string.Format("Attempting to remove listener with for event type {0} but current listener is null.", eventEnum));
                    }
                    else if (d.GetType() != listenerBeingRemoved.GetType())
                    {
                        throw new ListenerException(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventEnum, d.GetType().Name, listenerBeingRemoved.GetType().Name));
                    }
                }
                else
                {
                    throw new ListenerException(string.Format("Attempting to remove listener for type {0} but Messenger doesn't know about this event type.", eventEnum));
                }
            }
            else
            {
                throw new ListenerException(string.Format("Attempting to remove listener {0} with type {1} but Messenger doesn't know about this type.", eventEnum, t));
            }
        }

        static public void OnListenerRemoved(Type t, Enum enumEvent)
        {
            if (eventTable[t][enumEvent] == null)
            {
                eventTable[t].Remove(enumEvent);
            }

            if (eventTable[t] == null)
            {
                eventTable.Remove(t);
            }

        }

        static public void OnBroadcasting(Enum eventEnum, MessengerMode mode)
        {
            Type t = eventEnum.GetType();

            if (mode == MessengerMode.REQUIRE_LISTENER && 
                (!eventTable.ContainsKey(t) || !eventTable[t].ContainsKey(eventEnum)))
            {
                throw new MessengerInternal.BroadcastException(string.Format("Broadcasting message {0} but no listener found.", eventEnum.ToString()));
            }
        }

        static public BroadcastException CreateBroadcastSignatureException(string eventType)
        {
            return new BroadcastException(string.Format("Broadcasting message {0} but listeners have a different signature than the broadcaster.", eventType));
        }

        public class BroadcastException : Exception
        {
            public BroadcastException(string msg)
                : base(msg)
            {
            }
        }

        public class ListenerException : Exception
        {
            public ListenerException(string msg)
                : base(msg)
            {
            }
        }
    }

    // No parameters
    static public class Messenger
    {
        static public void AddListener(Enum eventType, Action handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        static public void AddListener<TReturn>(Enum eventType, Func<TReturn> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        static public void RemoveListener(Enum eventType, Action handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        static public void RemoveListener<TReturn>(Enum eventType, Func<TReturn> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        static public void Broadcast(Enum eventType)
        {
            Broadcast(eventType, MessengerInternal.DEFAULT_MODE);
        }

        static public void Broadcast<TReturn>(Enum eventType, Action<TReturn> returnCall)
        {
            Broadcast(eventType, returnCall, MessengerInternal.DEFAULT_MODE);
        }

        static public void Broadcast(Enum eventType, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Action>(eventType);

            foreach (var callback in invocationList)
                callback.Invoke();
        }

        static public void Broadcast<TReturn>(Enum eventType, Action<TReturn> returnCall, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Func<TReturn>>(eventType);

            foreach (var result in invocationList.Select(del => del.Invoke()).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }

    static public bool TryRemoveListener(Enum eventType, Action Handler)
    {
        try
        {
            RemoveListener(eventType, Handler);
        }
        catch (System.NullReferenceException)
        {
            //Debug.Log("Listener " + eventType.ToString() + " already removed");
            return false;
        }
        return true;
    }

    static public bool TryRemoveListener<TResult>(Enum eventType, Func<TResult> Handler)
    {
        try
        {
            RemoveListener(eventType, Handler);
        }
        catch (System.NullReferenceException)
        {
            //Debug.Log("Listener " + eventType.ToString() + " already removed");
            return false;
        }

        return true;
    }
}

    // One parameter
    static public class Messenger<T>
    {

        static public void AddListener(Enum eventType, Action<T> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        static public void AddListener<TReturn>(Enum eventType, Func<T, TReturn> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        static public void RemoveListener(Enum eventType, Action<T> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        static public void RemoveListener<TReturn>(Enum eventType, Func<T, TReturn> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        static public void Broadcast(Enum eventType, T arg1)
        {
            Broadcast(eventType, arg1, MessengerInternal.DEFAULT_MODE);
        }

        static public void Broadcast<TReturn>(Enum eventType, T arg1, Action<TReturn> returnCall)
        {
            Broadcast(eventType, arg1, returnCall, MessengerInternal.DEFAULT_MODE);
        }

        static public void Broadcast(Enum eventType, T arg1, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Action<T>>(eventType);

            foreach (var callback in invocationList)
                callback.Invoke(arg1);
        }

        static public void Broadcast<TReturn>(Enum eventType, T arg1, Action<TReturn> returnCall, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Func<T, TReturn>>(eventType);

            foreach (var result in invocationList.Select(del => del.Invoke(arg1)).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }

    /// <summary>
    /// returns false if can't remove
    /// TODO: FIX catch - another exception
    /// </summary>
    /// <param name="eventType"></param>
    /// <returns></returns>
    static public bool TryRemoveListener(Enum eventType, Action<T> Handler)
        {
            try
            {
                Messenger<T>.RemoveListener(eventType, Handler);
             }
             catch (System.NullReferenceException)
            {
                //Debug.Log("Listener " + eventType.ToString() + " already removed");
                return false;
            }
            return true;
        }

    static public bool TryRemoveListener<TResult>(Enum eventType, Func<T, TResult> Handler)
    {
        try
        {
            Messenger<T>.RemoveListener(eventType, Handler);
        }
        catch (System.NullReferenceException)
        {
            //Debug.Log("Listener " + eventType.ToString() + " already removed");
            return false;
        }
        return true;
    }

}


    // Two parameters
    static public class Messenger<T, U>
    {
        static public void AddListener(Enum eventType, Action<T, U> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        static public void AddListener<TReturn>(Enum eventType, Func<T, U, TReturn> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        static public void RemoveListener(Enum eventType, Action<T, U> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        static public void RemoveListener<TReturn>(Enum eventType, Func<T, U, TReturn> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        static public void Broadcast(Enum eventType, T arg1, U arg2)
        {
            Broadcast(eventType, arg1, arg2, MessengerInternal.DEFAULT_MODE);
        }

        static public void Broadcast<TReturn>(Enum eventType, T arg1, U arg2, Action<TReturn> returnCall)
        {
            Broadcast(eventType, arg1, arg2, returnCall, MessengerInternal.DEFAULT_MODE);
        }

        static public void Broadcast(Enum eventType, T arg1, U arg2, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Action<T, U>>(eventType);

            foreach (var callback in invocationList)
                callback.Invoke(arg1, arg2);
        }

        static public void Broadcast<TReturn>(Enum eventType, T arg1, U arg2, Action<TReturn> returnCall, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Func<T, U, TReturn>>(eventType);

            foreach (var result in invocationList.Select(del => del.Invoke(arg1, arg2)).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }

    static public bool TryRemoveListener(Enum eventType, Action<T, U> Handler)
    {
        try
        {
            RemoveListener(eventType, Handler);
        }
        catch (System.NullReferenceException)
        {
            //Debug.Log("Listener " + eventType.ToString() + " already removed");
            return false;
        }
        return true;
    }

    static public bool TryRemoveListener<TResult>(Enum eventType, Func<T, U, TResult> Handler)
    {
        try
        {
            RemoveListener(eventType, Handler);
        }
        catch (System.NullReferenceException)
        {
            //Debug.Log("Listener " + eventType.ToString() + " already removed");
            return false;
        }
        return true;
    }

}


    // Three parameters
    static public class Messenger<T, U, V>
    {
        static public void AddListener(Enum eventType, Action<T, U, V> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        static public void AddListener<TReturn>(Enum eventType, Func<T, U, V, TReturn> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        static public void RemoveListener(Enum eventType, Action<T, U, V> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        static public void RemoveListener<TReturn>(Enum eventType, Func<T, U, V, TReturn> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        static public void Broadcast(Enum eventType, T arg1, U arg2, V arg3)
        {
            Broadcast(eventType, arg1, arg2, arg3, MessengerInternal.DEFAULT_MODE);
        }

        static public void Broadcast<TReturn>(Enum eventType, T arg1, U arg2, V arg3, Action<TReturn> returnCall)
        {
            Broadcast(eventType, arg1, arg2, arg3, returnCall, MessengerInternal.DEFAULT_MODE);
        }

        static public void Broadcast(Enum eventType, T arg1, U arg2, V arg3, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Action<T, U, V>>(eventType);

            foreach (var callback in invocationList)
                callback.Invoke(arg1, arg2, arg3);
        }

        static public void Broadcast<TReturn>(Enum eventType, T arg1, U arg2, V arg3, Action<TReturn> returnCall, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Func<T, U, V, TReturn>>(eventType);

            foreach (var result in invocationList.Select(del => del.Invoke(arg1, arg2, arg3)).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }

    static public bool TryRemoveListener(Enum eventType, Action<T, U, V> Handler)
    {
        try
        {
            RemoveListener(eventType, Handler);
        }
        catch (System.NullReferenceException)
        {
            //Debug.Log("Listener " + eventType.ToString() + " already removed");
            return false;
        }
        return true;
    }

    static public bool TryRemoveListener<TResult>(Enum eventType, Func<T, U,V, TResult> Handler)
    {
        try
        {
            RemoveListener(eventType, Handler);
        }
        catch (System.NullReferenceException)
        {
            //Debug.Log("Listener " + eventType.ToString() + " already removed");
            return false;
        }
        return true;
    }

}
