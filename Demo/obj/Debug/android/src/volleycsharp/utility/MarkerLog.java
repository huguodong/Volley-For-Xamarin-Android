package volleycsharp.utility;


public class MarkerLog
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_finalize:()V:GetJavaFinalizeHandler\n" +
			"";
		mono.android.Runtime.register ("VolleyCSharp.Utility.MarkerLog, VolleyCSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MarkerLog.class, __md_methods);
	}


	public MarkerLog () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MarkerLog.class)
			mono.android.TypeManager.Activate ("VolleyCSharp.Utility.MarkerLog, VolleyCSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void finalize ()
	{
		n_finalize ();
	}

	private native void n_finalize ();

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
