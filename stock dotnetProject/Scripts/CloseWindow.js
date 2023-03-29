// JScript File

window.onbeforeunload=close


function close(e)
{
    var keyCd = null;
    var altKy = false;
    var ctlKy = false;
    var shiftKy = false;
    keyCd = event.keyCode;
    altKy = event.altKey;
    ctlKy = event.ctlKey;
    shiftKy = event.shiftKey;
    if((window.event.clientX<0) || (window.event.clientY<0))
    {
        document.location.href='../closewindow.aspx';
    }
    else if ((altKy || keyCd == 115))
    {       
	    document.location.href='../closewindow.aspx';
    }
}
