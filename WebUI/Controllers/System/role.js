
var namepa = "SystemBLL.SysJueSe";
var dllname = "SystemBLL";

var tTable = null;


function CreateTable() {
    tTable = new aceTable("table");
    tTable.cols.push(new Cols("100", '角色编号', '', 'jsID', '', null));
    tTable.cols.push(new Cols("200", '角色名称', '', 'jsName', '', null));
    tTable.cols.push(new Cols(null, '角色描述', '', 'jsMeno', '', null));
    tTable.name = "tTable";
    tTable.itemget = function (btn) {
        var item = tTable.GetItemByBtn(btn);
        window.top.OpenDialog("角色编辑", "View/System/RoleSet.html?id=" + item.jsID, window, null);
    }

    tTable.itemupdate = function (btn) {
        var item = tTable.GetItemByBtn(btn);
        OpenUserEdit(item.jsID);
    }
    tTable.itemdelete = function (btn) {
        var item = tTable.GetItemByBtn(btn);
        if (confirm("是否确定删除？")) {
            var request = new DBRequest(namepa, "Update", dllname);
            request.add("info", { jsID: item.jsID, jsISTrue: false });
            request.send(function () {
                BindTable();
            })
        }
    }
    tTable.initCreate();
}
function BindTable() {
    var request = new DBRequest(namepa, "GetListAll", dllname);
    request.send(function (ret) {
        if (ret.d != null) {
            tTable.items.length = 0;
            for (var i = 0; i < ret.d.length; i++) {
                tTable.items.push({ 'jsID': ret.d[i].jsID, 'jsName': ret.d[i].jsName, 'jsMeno': ret.d[i].jsMeno });
            }
            tTable.DataBind();
        }
    });
}

function OpenUserEdit(id) {
    window.top.OpenDialog("角色编辑", "View/System/RoleEdit.html?id=" + id, window, function () {
        BindTable();
    });
}

//List-End


var nowInfo = null;



function Get() {
    var req = GetRequest();
    var jsID = req["id"];
    var request = new DBRequest(namepa, "Get", dllname);
    request.add("vID", jsID);
    request.send(function (ret) {
        nowInfo = ret.d;
        _setPage();
    });
}

function _setPage() {
    document.getElementById("jsName").value = nowInfo.jsName;
    document.getElementById("jsMeno").value = (nowInfo.jsMeno == null ? "" : nowInfo.jsMeno);
}
function _getPage() {
    nowInfo.jsName = document.getElementById("jsName").value;
    nowInfo.jsMeno = document.getElementById("jsMeno").value;
}

function Save() {
    _getPage();
    if (nowInfo.jsName == "") {
        alert("请录入角色名称！");
        return;
    }
    var request = null;
    if (!nowInfo.jsID)
        request = new DBRequest(namepa, "Insert", dllname);
    else
        request = new DBRequest(namepa, "Update", dllname);
    request.add("info", nowInfo);
    request.send(function (ret) {
        window.top.CloseDialog();
    });

}
///Edit--End

var litext = "<span class='menu-text'>[cdName]</span>　<input type='checkbox' value='[cdID]' onclick='setTop(this)' />";

function setLoad() {
    var req = GetRequest();
    var jsID = req["id"];
    var request = new DBRequest(dllname + ".CaiDan", "GetListAll", dllname);
    request.send(function (ret) {
        cdList = ret.d;
        SetTree();
        request = new DBRequest(dllname + ".SysJueSeCaiDan", "GetCaiDanByJs", dllname);
        request.add("JsID", jsID);
        request.send(function (r) {
            cdjs = r.d;
            SetTreeValue();
        });
    });
}

function setTop(check) {
    if (check.parentNode.getElementsByTagName("li").length != 0) {
        var cheList = check.parentNode.getElementsByTagName("input");
        for (var i = 0; i < cheList.length; i++) {
            if (cheList[i].type == "checkbox") {
                cheList[i].checked = check.checked;
            }
        }
    } else {
        var cheList = check.parentNode.parentNode.getElementsByTagName("input");
        var isChecked = false;
        for (var i = 0; i < cheList.length; i++) {
            if (cheList[i].type == "checkbox" && cheList[i].checked) {
                isChecked = true;
                break;
            }
        }
        cheList = check.parentNode.parentNode.parentNode.getElementsByTagName("input");
        for (var i = 0; i < cheList.length; i++) {
            if (cheList[i].type == "checkbox") {
                cheList[i].checked = isChecked;
                break;
            }
        }

    }
}
var cdList = null;
var cdjs = null;
function SetTree() {
    var treeHTML = getNextItem(0, cdList);
    document.getElementById("ulnav").innerHTML = treeHTML;
}
function SetTreeValue() {
    var check = document.getElementById("ulnav").getElementsByTagName("input");
    for (var i = 0; i < check.length; i++) {
        for (var j = 0; j < cdjs.length; j++) {
            if (check[i].value == cdjs[j].cdID) {
                check[i].checked = true;
                break;
            }
        }
    }
}
function getNextItem(pid, caidans) {
    var retStr = "";
    for (var i = 0; i < caidans.length; i++) {
        if (caidans[i].cdPID == pid) {
            var x = getNextItem(caidans[i].cdID, caidans);
            if (x != "") {
                retStr = retStr + "<li>" + (litext.replace("[cdName]", caidans[i].cdName).replace("[cdID]", caidans[i].cdID) + "<ul class='submenu'>" + x + "</ul>") + "</li>";
            } else {
                retStr = retStr + "<li>" + (litext.replace("[cdName]", caidans[i].cdName).replace("[cdID]", caidans[i].cdID)) + "</li>";
            }
        }
    }
    return retStr;
}
function SaveTree() {
    var req = GetRequest();
    var jsID = req["id"];
    var check = document.getElementById("ulnav").getElementsByTagName("input");
    var list = [];
    for (var i = 0; i < check.length; i++) {
        if (check[i].type == "checkbox") {
            if (check[i].checked) {
                list.push({ JsID: jsID, cdID: check[i].value });
            }
        }
    }
    var request = new DBRequest(dllname + ".SysJueSeCaiDan", "Save", dllname);
    request.add("list", list);
    request.send(function (ret) {
        window.top.CloseDialog();
    });

}