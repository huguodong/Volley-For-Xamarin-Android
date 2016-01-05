package volleycsharp.delivery;


public class ExecutorDelivery
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		java.util.concurrent.Executor
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_execute:(Ljava/lang/Runnable;)V:GetExecute_Ljava_lang_Runnable_Handler:Java.Util.Concurrent.IExecutorInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("VolleyCSharp.Delivery.ExecutorDelivery, VolleyCSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ExecutorDelivery.class, __md_methods);
	}


	public ExecutorDelivery () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ExecutorDelivery.class)
			mono.android.TypeManager.Activate ("VolleyCSharp.Delivery.ExecutorDelivery, VolleyCSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ExecutorDelivery (android.os.Handler p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == ExecutorDelivery.class)
			mono.android.TypeManager.Activate ("VolleyCSharp.Delivery.ExecutorDelivery, VolleyCSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.OS.Handler, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

	public ExecutorDelivery (java.util.concurrent.Executor p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == ExecutorDelivery.class)
			mono.android.TypeManager.Activate ("VolleyCSharp.Delivery.ExecutorDelivery, VolleyCSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Java.Util.Concurrent.IExecutor, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void execute (java.lang.Runnable p0)
	{
		n_execute (p0);
	}

	private native void n_execute (java.lang.Runnable p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
