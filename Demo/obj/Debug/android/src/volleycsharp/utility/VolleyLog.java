package volleycsharp.utility;


public class VolleyLog
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("VolleyCSharp.Utility.VolleyLog, VolleyCSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", VolleyLog.class, __md_methods);
	}


	public VolleyLog () throws java.lang.Throwable
	{
		super ();
		if (getClass () == VolleyLog.class)
			mono.android.TypeManager.Activate ("VolleyCSharp.Utility.VolleyLog, VolleyCSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
