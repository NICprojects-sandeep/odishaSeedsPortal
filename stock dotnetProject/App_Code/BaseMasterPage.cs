using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public class BaseMasterPage : MasterPage
{
	protected HiddenField hiddenSession;

	public string SessionText
	{
		get
		{
			return this.hiddenSession.Value;
		}
	}

	public BaseMasterPage()
	{
	}
}