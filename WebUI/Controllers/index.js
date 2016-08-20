var __UserInfo = null;
function GetUserInfo() {
    if (__UserInfo == null) {
        alert("您尚未登录，请重新登陆！");
        location = "Login.html";
        return;
    }
    return __UserInfo;
}

var dhMb = "<li class='active'>" +
           "   <a href='[URL]' target='mainpage'>" +
           "       <i class='[img]'></i>" +
           "       [name]" +
           "   </a>" +
           "</li>";

var dhFMb = "<a href='#' class='dropdown-toggle'>" +
            "   <i class='[img]'></i>" +
            "   <span class='menu-text'> [name] </span>" +
            "   <b class='arrow icon-angle-down'></b>" +
            "</a>";

var nowDialog = {
    nDialog: null,
    nWin: null,
    retFun: null
};
//填出框
function OpenDialog(title, url, win, retFun, height) {
    var h = (height ? height : "400");
    if (!url) { alert("请录入URL"); return; }
    var strFrame = "<iframe style='width:100%;height:[height]px;' src='[url]'  frameborder='0'></iframe>";
    nowDialog.nDialog = bootbox.dialog((strFrame.replace("[height]", h).replace("[url]", url)));
    nowDialog.nWin = win;
    nowDialog.retFun = retFun;
}
function CloseDialog() {
    bootbox.hideAll();
    if (nowDialog.nWin && nowDialog.retFun) {
        var fun = nowDialog.nWin.eval(nowDialog.retFun);
        new fun();
    }
}
//格式化
function jsonDateFormat(jsonDate) {//json日期格式转换为正常格式
    try {//出自http://www.cnblogs.com/ahjesus 尊重作者辛苦劳动成果,转载请注明出处,谢谢!
        var date = new Date(parseInt(jsonDate.replace("/Date(", "").replace(")/", ""), 10));
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        var hours = date.getHours();
        var minutes = date.getMinutes();
        var seconds = date.getSeconds();
        var milliseconds = date.getMilliseconds();
        return date.getFullYear() + "-" + month + "-" + day;
    } catch (ex) {//出自http://www.cnblogs.com/ahjesus 尊重作者辛苦劳动成果,转载请注明出处,谢谢!
        return "";
    }
}

function Load() {
    var request = new DBRequest("SystemBLL.SysUser", "GetUser", "SystemBLL");
    request.send(function (ret) {
        __UserInfo = ret.d;
        var user = GetUserInfo();
        if (user) {
            var strHTML = "";
            if (user.CaiDans.length != 0) {
                strHTML = getNextItem(0, user.CaiDans);
            }
            document.getElementById("ulnav").innerHTML = strHTML;
        }
        document.getElementById("userName").innerHTML = user.UserName;
    });
}

function getNextItem(pid, caidans) {
    var retStr = "";
    for (var i = 0; i < caidans.length; i++) {
        if (caidans[i].cdPID == pid) {
            var x = getNextItem(caidans[i].cdID, caidans);
            if (x != "") {
                retStr = retStr + "<li>" + (dhFMb.replace("[img]", caidans[i].cdImg).replace("[name]", caidans[i].cdName) + "<ul class='submenu'>" + x + "</ul>") + "</li>";
            } else {
                retStr = retStr + (dhMb.replace("[img]", caidans[i].cdImg).replace("[name]", caidans[i].cdName).replace("[URL]", caidans[i].cdURL));
            }
        }
    }
    return retStr;
}


function UpdatePassword() {
    window.top.OpenDialog("修改密码", "View/System/UpdateUserPass.html", window, null);
}

function Logout() {
    if (confirm("是否确定注销？")) {
        var request = new DBRequest("SystemBLL.SysUser", "Logout", "SystemBLL");
        request.send(function (ret) {
            location = "login.html";
        });
    }
}