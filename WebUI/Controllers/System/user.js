var tTable = null;
function CreateTable() {
    tTable = new aceTable("table");
    tTable.cols.push(new Cols("100", '用户编号', '', 'yhID', '', null));
    tTable.cols.push(new Cols("200", '用户账号', '', 'yhLoginName', '', null));
    tTable.cols.push(new Cols(null, '用户名称', '', 'yhName', '', null));
    tTable.cols.push(new Cols('100', '最后登录日期', '', 'yhLastLgoinTime', 'center', function (t) {
        if (t)
            return window.top.jsonDateFormat(t);
        return "";
    }));
    tTable.name = "tTable";
    //tTable.itemget = function (btn) {
    //    var item = tTable.GetItemByBtn(btn);
    //    alert("查看");
    //}
    tTable.itemupdate = function (btn) {
        var item = tTable.GetItemByBtn(btn);
        OpenUserEdit(item.yhID);
    }
    tTable.itemdelete = function (btn) {
        var item = tTable.GetItemByBtn(btn);
        if (confirm("是否确定删除？")) {
            var request = new DBRequest("SystemBLL.SysYongHu", "Delete", "SystemBLL");
            request.add("info", { yhID: item.yhID });
            request.send(function () {
                BindTable();
            })
        }
    }
    tTable.initCreate();
}
function BindTable() {
    var request = new DBRequest("SystemBLL.SysYongHu", "GetListAll", "SystemBLL");
    request.send(function (ret) {
        if (ret.d != null) {
            tTable.items.length = 0;
            for (var i = 0; i < ret.d.length; i++) {
                tTable.items.push({ 'yhID': ret.d[i].yhID, 'yhLoginName': ret.d[i].yhLoginName, 'yhName': ret.d[i].yhName, 'yhLastLgoinTime': ret.d[i].yhLastLgoinTime });
            }
            tTable.DataBind();
        }
    });
}

function OpenUserEdit(id) {
    window.top.OpenDialog("用户编辑", "View/System/UserEdit.html?id=" + id, window, function () {
        BindTable();
    });
}
//List-End

var nowUser = null;



function Get() {
    var req = GetRequest();
    var yhID = req["id"];
    var request = new DBRequest("SystemBLL.SysYongHu", "Get", "SystemBLL");
    request.add("vID", yhID);
    request.send(function (ret) {
        nowUser = ret.d;
        if (ret.d.yhID)
            document.getElementById("yhLoginName").disabled = true;
        _setPage();
    });
}

function _setPage() {
    document.getElementById("yhName").value = nowUser.yhName;
    document.getElementById("yhLoginName").value = nowUser.yhLoginName;
}
function _getPage() {
    nowUser.yhName = document.getElementById("yhName").value;
    nowUser.yhLoginName = document.getElementById("yhLoginName").value;
}

function Save() {
    _getPage();
    nowUser.yhLastLgoinTime = null;
    var request = null;
    if (!nowUser.yhID)
        request = new DBRequest("SystemBLL.SysYongHu", "Insert", "SystemBLL");
    else
        request = new DBRequest("SystemBLL.SysYongHu", "Update", "SystemBLL");
    request.add("info", nowUser);
    request.send(function (ret) {
        window.top.CloseDialog();
    });

}

//Edit--End

function SavePass(ymm, xmm, txmm) {
    var request = new DBRequest("SystemBLL.SysYongHu", "UpdatePassword", "SystemBLL");
    if (getcon(ymm).value == "") {
        alert("请录入原密码！");
        return;
    }
    if (getcon(xmm).value == "") {
        alert("请录入新密码！");
        return;
    }
    if (getcon(txmm).value == "") {
        alert("请录入确认新密码！");
        return;
    }
    if (getcon(txmm).value != getcon(xmm).value) {
        alert("新密码与确认信密码值不相同，无法保存！");
        return;
    }
    request.add("ymm", getcon(ymm).value);
    request.add("xmm", getcon(xmm).value);
    request.send(function (ret) {
        alert("修改成功，请重新登陆！");
        var request = new DBRequest("SystemBLL.SysUser", "Logout", "SystemBLL");
        request.send(function (ret) {
            window.top.location = "/login.html";
        });
    })
}


function getcon(ids) {
    return document.getElementById(ids);
}