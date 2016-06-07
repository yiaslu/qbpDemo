var oPopupMenu = null;
oPopupMenu = window.createPopup();

if (window.Event) {
    if (window.captureEvents) {
        document.captureEvents(window.Event.mouseup);
    }
}


function ControlSelectedParent() {
    if (document.selection) {
        if (document.selection.type == "Control") {
            var oControlRange = document.selection.createRange();
            return oControlRange(0).parentElement;
        }
        else {
            return document.selection.createRange().parentElement();
        }
    }
}

function ControlSelectedParentTag() {
    if (document.selection) {
        if (document.selection.type == "Control") {
            var oControlRange = document.selection.createRange();
            return oControlRange(0).parentElement.tagName;
        }
        else {
            return document.selection.createRange().parentElement().tagName;
        }
    }
}

//
function ControlSelectedTag() {
    if (document.selection && document.selection.type == "Control") {
        var oControlRange = document.selection.createRange();
        return oControlRange(0).tagName.toUpperCase();
    }
}

//
function ControlSelectedType() {
    if (document.selection && document.selection.type == "Control") {
        var oControlRange = document.selection.createRange();
        if (oControlRange(0).type != null)
            return oControlRange(0).type.toUpperCase();
        else
            return "";
    }
}

function ControlSelectedInputType() {
    if (document.selection && document.selection.type == "Control") {
        var oControlRange = document.selection.createRange();
        try {
            return oControlRange(0).inputtype.toUpperCase();
        }
        catch (e) {
            return "";
        }
    }
}

function hideContextMenu() {
    oPopupMenu.hide();
    return false;
}



// 菜单常量
var sMenu1 = "<TABLE width=150 border=0 cellpadding=0 cellspacing=0>";
var sMenu2 = "</TABLE>";
var sMenuHr = "<tr><td align=center valign=middle height=1 class=HrShadow></td></tr>" +
    "<tr><td align=center valign=middle height=1 class=HrHighLight></td></tr>";

//右键
function showContextMenu(event) {
    //if (switchMode.checked) return false;

    var width = 150;
    var height = 0;
    var lefter = event.clientX;
    var topper = event.clientY;

    var oPopDocument = oPopupMenu.document;
    var oPopBody = oPopupMenu.document.body;

    var sMenu = "";

    sMenu += getFormatMenuRow("cut", "剪切");
    sMenu += getFormatMenuRow("copy", "复制");
    sMenu += getFormatMenuRow("paste", "粘贴");
    sMenu += getFormatMenuRow("delete", "删除");
    sMenu += getFormatMenuRow("selectall", "全选");
    var tag = ControlSelectedTag();
    var tp = ControlSelectedType();
    var pr = ControlSelectedParentTag();
    if (pr == "TD" || pr == "TH") {
        sMenu += sMenuHr;
        sMenu += getMenuRow("", "insertrow()", "插入行");
        sMenu += getMenuRow("", "insertcol()", "插入列");
        sMenu += getMenuRow("", "deleterow()", "删除行");
        sMenu += getMenuRow("", "deletecol()", "删除列");
        sMenu += getMenuRow("", "insertitem()", "插入单元格");
        sMenu += getMenuRow("", "deleteitem()", "删除单元格");
        sMenu += sMenuHr;
        sMenu += getMenuRow("", "ShowParentDialog('DtableItem.htm',360,200)", "单元格设置");
        sMenu += getMenuRow("", "ShowParentDialog('Dtable.htm',360,220)", "表格设置");
        height += 170;
    }
    var inputtype = ControlSelectedInputType();
    if (tag == "INPUT" && tp == "TEXT" && inputtype == "TEXT" && document.queryCommandEnabled("copy")) {
        sMenu += sMenuHr;
        sMenu += getMenuRow("", "ShowParentDialog('InputItemText.htm',360,270)", "单行输入框属性");
        height += 22;
    }
    else if (tag == "TEXTAREA" && tp == "TEXTAREA" && inputtype == "TEXTAREA" && document.queryCommandEnabled("copy")) {
        sMenu += sMenuHr;
        sMenu += getMenuRow("", "ShowParentDialog('InputItemTextarea.htm',360,270)", "多行输入框属性");
        height += 22;
    }
    else if (tag == "SELECT" && tp == "SELECT-ONE" && inputtype == "SELECT" && document.queryCommandEnabled("copy")) {
        sMenu += sMenuHr;
        sMenu += getMenuRow("", "ShowParentDialog('InputItemSelect.htm',360,310)", "下拉菜单属性");
        height += 22;
    }
    else if (tag == "INPUT" && tp == "BUTTON" && inputtype == "RADIO" && document.queryCommandEnabled("copy")) {
        sMenu += sMenuHr;
        sMenu += getMenuRow("", "ShowParentDialog('InputItemRadio.htm',360,300)", "单项选择框属性");
        height += 22;
    }
    else if (tag == "INPUT" && tp == "BUTTON" && inputtype == "CHECKBOX" && document.queryCommandEnabled("copy")) {
        sMenu += sMenuHr;
        sMenu += getMenuRow("", "ShowParentDialog('InputItemCheckbox.htm',360,300)", "多项选择框属性");
        height += 22;
    }
    else if (tag == "INPUT" && tp == "BUTTON"
	        && (inputtype == "DATE" || inputtype == "TIME" || inputtype == "DATETIME")
	        && document.queryCommandEnabled("copy")) {
        sMenu += sMenuHr;
        sMenu += getMenuRow("", "ShowParentDialog('InputItemDateTime.htm',360,220)", "日历控件属性");
        height += 22;
    }
    else if (tag == "INPUT" && tp == "BUTTON" && inputtype == "SIGN" && document.queryCommandEnabled("copy")) {
        sMenu += sMenuHr;
        sMenu += getMenuRow("", "ShowParentDialog('InputItemSign.htm',360,220)", "签章控件属性");
        height += 22;
    }
    else if (tag == "INPUT" && tp == "TEXT" && inputtype == "ACER" && document.queryCommandEnabled("copy")) {
        sMenu += sMenuHr;
        sMenu += getMenuRow("", "ShowParentDialog('InputItemAcer.htm',360,220)", "宏控件属性");
        height += 22;
    }
    else if (tag == "INPUT" && tp == "TEXT" && inputtype == "CounterSign" && document.queryCommandEnabled("copy")) {
        sMenu += getMenuRow("", "ShowParentDialog('InputItemCounterSign.htm',360,220)", "会签控件属性");
        sMenu += sMenuHr;
        height += 22;
    }
    else if (tag == "INPUT" && tp == "TEXT" && inputtype == "ORG" && document.queryCommandEnabled("copy")) {
        sMenu += getMenuRow("", "ShowParentDialog('InputItemOrg.htm',360,250)", "组织结构控件属性");
        sMenu += sMenuHr;
        height += 22;
    }
    else if (tag == "INPUT" && tp == "TEXT" && inputtype == "SYS" && document.queryCommandEnabled("copy")) {
        sMenu += sMenuHr;
        sMenu += getMenuRow("", "ShowParentDialog('InputItemSys.htm',360,250)", "系统选择控件属性");
        height += 22;
    }
    else if (tag == "INPUT" && tp == "BUTTON" && inputtype == "TABLE" && document.queryCommandEnabled("copy")) {
        sMenu += sMenuHr;
        sMenu += getMenuRow("", "ShowParentDialog('InputItemTable.htm',360,220)", "数据表属性");
        height += 22;
    }
    else if (tag == "INPUT" && tp == "TEXT" && inputtype == "VERIFY" && document.queryCommandEnabled("copy")) {
        sMenu += sMenuHr;
        sMenu += getMenuRow("", "ShowParentDialog('InputItemVerify.htm',360,220)", "计算控件属性");
        height += 22;
    }
    else if (tag == "TABLE" && tp == "" && document.queryCommandEnabled("copy")) {
        sMenu += sMenuHr;
        sMenu += getMenuRow("", "ShowParentDialog('Dtable.htm',360,220)", "表格设置");
        height += 22;
    }
    else {
        //alert(tag+tp);
        sMenu += sMenuHr;
    }
    sMenu = sMenu1 + sMenu + sMenu2;
    height += 100;
    oPopDocument.open();
    oPopDocument.write("<head><link href=\"MenuArea.css\" type=\"text/css\" rel=\"stylesheet\"></head>" +
        "<body scroll=\"no\" onConTextMenu=\"event.returnValue=false;\">" + sMenu);
    oPopDocument.close();

    height += 2;
    if (lefter + width > document.body.clientWidth) lefter = lefter - width;

    oPopupMenu.show(lefter, topper, width, height, document.body);
    return false;
}

// 取标准的formatReport菜单行
function getFormatMenuRow(menu, html) {
    var s_Disabled = "";
    if (!document.queryCommandEnabled(menu)) {
        s_Disabled = "disabled";
    }
    var s_Event = "formatReport('" + menu + "')";
    return getMenuRow(s_Disabled, s_Event, html);

}

// 取菜单行
function getMenuRow(s_Disabled, s_Event, s_Html) {
    var s_MenuRow = "";
    s_MenuRow = "<tr " + s_Disabled + "><td valign=middle height=20 class=RightBg onMouseOver=this.className='MouseOver'; onMouseOut=this.className='MouseOut';";
    if (s_Disabled == "") {
        s_MenuRow += " onclick=\"parent." + s_Event + ";parent.oPopupMenu.hide();\"";
    }
    s_MenuRow += ">";

    if (s_Html == "复制")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/icon_copy.gif>";
    else if (s_Html == "剪切")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/icon_cut.gif>";
    else if (s_Html == "粘贴")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/icon_paste.gif>";
    else if (s_Html == "删除")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/icon_delete.gif>";
    else if (s_Html == "全选")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/icon_select_all.gif>";

    else if (s_Html == "图片属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/icon_ins_image.gif>";
    else if (s_Html == "单行输入框属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/textfield.gif>";
    else if (s_Html == "多行输入框属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/textarea.gif>";
    else if (s_Html == "下拉菜单属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/listmenu.gif>";
    else if (s_Html == "多项选择框属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/checkbox.gif>";
    else if (s_Html == "单项选择框属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/checkbox.gif>";
    else if (s_Html == "日历控件属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/calendar.gif>";
    else if (s_Html == "宏控件属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/auto.gif>";
    else if (s_Html == "会签控件属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/countersign.gif>";
    else if (s_Html == "计算控件属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/calc.gif>";
    else if (s_Html == "列表控件属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/listview.gif>";
    else if (s_Html == "组织结构控件属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/user.gif>";
    else if (s_Html == "系统选择控件属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/sys.gif>";
    else if (s_Html == "签章控件属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/sign.gif>";
    else if (s_Html == "数据表属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/listview.gif>";
    else if (s_Html == "计算控件属性")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/calc.gif>";
    else if (s_Html == "表格设置")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/bg.gif>";
    else if (s_Html == "删除行")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/deleterow.gif>";
    else if (s_Html == "删除列")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/deletecol.gif>";
    else if (s_Html == "插入行")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/insertrow.gif>";
    else if (s_Html == "插入列")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/insertcol.gif>";
    else if (s_Html == "插入单元格")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/insertitem.gif>";
    else if (s_Html == "删除单元格")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/deleteitem.gif>";
    else if (s_Html == "单元格设置")
        s_MenuRow += "<img border=0 width=20 height=20 align=absmiddle src=/images/gzl/dyg.gif>";

    s_MenuRow += "&nbsp;" + s_Html + "</td></tr>";
    return s_MenuRow;


}
// 格式化编辑器中的内容，并执行
function formatReport(what, opt) {
    //if(!RichEditor.txtView) return;
    if (opt == "RemoveFormat") {
        what = opt;
        opt = null;
    }
    if (opt == null) document.execCommand(what);
    else document.execCommand(what, "", opt);
    //reset();
}

function ShowParentDialog(url, width, height) {
    var arr = window.showModalDialog(url, parent, "dialogWidth:" + width + "px;dialogHeight:" + height + "px;help:no;scroll:auto;resizable:1;status:no;dialogTop:" + (screen.availHeight - 160) / 2 + "px;dialogLeft:" + (screen.availWidth - 300) / 2 + "px");
    //window.top.OpenDiv('Div_Gzl',width, height, '','/Gzl/Bdsj/'+url,window);
    //return false;
}

function ShowDialog(url, width, height) {
    var arr = window.showModalDialog(url, self, "dialogWidth:" + width + "px;dialogHeight:" + height + "px;help:no;scroll:auto;resizable:1;status:no;dialogTop:" + (screen.availHeight - 160) / 2 + "px;dialogLeft:" + (screen.availWidth - 300) / 2 + "px");
    //window.top.OpenDiv('Div_Gzl',width, height, '','/Gzl/Bdsj/'+url,window);
    //return false;
}

function SetInputProperty(obj) {
    if (frameReport.document.body.contentEditable != "true") {
        frameReport.document.body.contentEditable = true;
    }
    frameReport.focus();
    if (frameReport.document.selection.type == "Control") return;

    if (obj == "InputItemText") {
        ShowDialog("InputItemText.htm", 360, 270);
    }
    else if (obj == "InputItemTextarea") {
        ShowDialog("InputItemTextarea.htm", 360, 270);
    }
    else if (obj == "InputItemCheckbox") {
        ShowDialog("InputItemCheckbox.htm", 360, 300);
    }
    else if (obj == "InputItemRadio") {
        ShowDialog("InputItemRadio.htm", 360, 300);
    }
    else if (obj == "InputItemSelect") {
        ShowDialog("InputItemSelect.htm", 360, 310);
    }
    else if (obj == "InputItemSign") {
        ShowDialog("InputItemSign.htm", 360, 220);
    }
    else if (obj == "InputItemDateTime") {
        ShowDialog("InputItemDateTime.htm", 360, 220);
    }
    else if (obj == "InputItemAcer") {
        ShowDialog("InputItemAcer.htm", 360, 220);
    }
    else if (obj == "InputItemCounterSign") {
        ShowDialog("InputItemCounterSign.htm", 360, 220);
    }
    else if (obj == "InputItemOrg") {
        ShowDialog("InputItemOrg.htm", 360, 250);
    }
    else if (obj == "InputItemSys") {
        ShowDialog("InputItemSys.htm", 360, 250);
    }
    else if (obj == "InputItemTable") {
        ShowDialog("InputItemTable.htm", 360, 220);
    }
    else if (obj == "InputItemVerify") {
        ShowDialog("InputItemVerify.htm", 360, 220);
    }
    else if (obj == "DTable") {
        ShowParentDialog('Dtable.htm', 360, 220);
    }
    if (frameReport.document.getElementsByName("formReport")[0] == undefined) {
        frameReport.document.body.innerHTML = "<form name=\"formReport\" method=\"post\" action=\"Save.aspx\">\n" + frameReport.document.body.innerHTML + "\n</form>";
    }
}

function Save() {
    var dicInputItems = new ActiveXObject("Scripting.Dictionary");
    //    if (frameReport.document.formReport == null) {
    //        alert("请添加表单控件！");
    //    }
//    var inputItems = frameReport.document.formReport.elements;
//    var i;
//    for (i = 0; i < inputItems.length; i++) {
//        if (inputItems[i].inputtype == "text" || inputItems[i].inputtype == "textarea"
//              || inputItems[i].inputtype == "table" || inputItems[i].inputtype == "date"
//              || inputItems[i].inputtype == "time" || inputItems[i].inputtype == "datetime") {
//            dicInputItems.Add(inputItems[i].inputname, inputItems[i].inputtype);
//        }
//        else if (inputItems[i].inputtype == "select") {
//            var j;
//            var opt = "";
//            for (j = 0; j < inputItems[i].options.length; j++) {
//                opt = opt + "," + inputItems[i].options[j].value;
//            }
//            dicInputItems.Add(inputItems[i].inputname, inputItems[i].inputtype + opt);
//        }
//        else if (inputItems[i].inputtype == "radio") {
//            if (!dicInputItems.Exists(inputItems[i].inputname)) {
//                dicInputItems.Add(inputItems[i].inputname, inputItems[i].inputtype + inputItems[i].value.replace(new RegExp("〇  ", "gm"), ",").replace(new RegExp("●  ", "gm"), ",").replace(new RegExp("\r\n", "gm"), ""));
//            }
//            //      else
//            //      {
//            //        dicInputItems.Item(inputItems[i].inputname) += ","+inputItems[i].value;
//            //      }
//        }
//        else if (inputItems[i].inputtype == "checkbox") {
//            if (!dicInputItems.Exists(inputItems[i].inputname)) {
//                dicInputItems.Add(inputItems[i].inputname, inputItems[i].inputtype + inputItems[i].value.replace(new RegExp("□  ", "gm"), ",").replace(new RegExp("▇  ", "gm"), ",").replace(new RegExp("\r\n", "gm"), ""));
//            }
//            //      else
//            //      {
//            //        dicInputItems.Item(inputItems[i].inputname) += ","+inputItems[i].value;
//            //      }
//        }
//        else if (inputItems[i].inputtype == "sign") {
//            dicInputItems.Add(inputItems[i].inputname, inputItems[i].inputtype + "," + inputItems[i].protectitems);
//        }
//        else if (inputItems[i].inputtype == "acer") {
//            dicInputItems.Add(inputItems[i].inputname, inputItems[i].inputtype + "," + inputItems[i].itemtype);
//        }
//        else if (inputItems[i].inputtype == "countersign") {
//            dicInputItems.Add(inputItems[i].inputname, inputItems[i].inputtype + "," + inputItems[i].itemtype);
//        }
//        else if (inputItems[i].inputtype == "org") {
//            dicInputItems.Add(inputItems[i].inputname, inputItems[i].inputtype + "," + inputItems[i].itemtype);
//        }
//        else if (inputItems[i].inputtype == "sys") {
//            dicInputItems.Add(inputItems[i].inputname, inputItems[i].inputtype + "," + inputItems[i].itemtype);
//        }
//        else if (inputItems[i].inputtype == "verify") {
//            dicInputItems.Add(inputItems[i].inputname, inputItems[i].inputtype + "," + inputItems[i].digital);
//        }

//        formToolbar.InputItems.value = "";
//        var arrInputItems = (new VBArray(dicInputItems.Keys())).toArray();
//        for (i = 0; i < arrInputItems.length; i++) {
//            if (i > 0) formToolbar.InputItems.value += "|";
//            formToolbar.InputItems.value += arrInputItems[i] + "," + dicInputItems.Item(arrInputItems[i]);
//        }
//        dicInputItems = null;
        formToolbar.ReportHtml.value = frameReport.document.getElementsByTagName("HTML")[0].innerHTML;
        formToolbar.isImport.value = "false";
        formToolbar.submit();
//    }
}

function Import() {
    if (formToolbar.fileMb.value == "") {
        alert("请选择模板文件！");
        formToolbar.fileMb.focus();
        return;
    }
    formToolbar.isImport.value = "true";
    formToolbar.submit();
}

function Preview(bdbh) {
    window.open(sysPath + "/ManageXt/Gzmb/Preview.aspx?bdbh=" + bdbh);
}

//function Open(lcbh)
//{
//    window.open(sysPath+"/Gzl/Lcsz/Flow.aspx?lcbh="+lcbh+"&tmp="+(new Date()).toString(), 'newwindow', 'height='+(screen.availHeight-50)+', width='+(screen.availWidth-10)+', top=0, left=0, toolbar=no, scrollbars=1, resizable=1,location=no, status=no'); 
//}

function createCal(bdbh) {
    ShowDialog("Dcalculate.aspx?bdbh=" + bdbh, 350, 220);
}
function createTable(bdbh) {
    SetInputProperty("DTable");
}
function createJs(bdbh) {
    ShowDialog("Djs.aspx?bdbh=" + bdbh, 350, 300);
}
function createCss(bdbh) {
    ShowDialog("Dcss.aspx?bdbh=" + bdbh, 350, 300);
}


function insertrow() {
    var td = ControlSelectedParent();
    var tr = td.parentNode;
    var table = tr.parentNode;
    //var len=table.rows[0].cells.length;   
    var newtr = table.insertRow(tr.rowIndex);
    for (var i = 0; i < tr.cells.length; i++) {
        newtr.insertCell(i);
    }
}

function insertcol() {
    var td = ControlSelectedParent();
    var tr = td.parentNode;
    var table = tr.parentNode;

    //var len=table.rows[0].cells.length;
    var col = td.cellIndex;
    var trs = table.rows;
    for (var i = 0; i < trs.length; i++) {
        if (col > trs[i].cells.length) {
            trs[i].insertCell(trs[i].cells.length);
        }
        else {
            trs[i].insertCell(col);
        }
    }
}

function deleterow() {
    var td = ControlSelectedParent();
    var tr = td.parentNode;
    var table = tr.parentNode;
    table.deleteRow(tr.rowIndex);
}

function deletecol() {
    var td = ControlSelectedParent();
    var tr = td.parentNode;
    var table = tr.parentNode;
    var col = td.cellIndex;
    var trs = table.rows;
    for (var i = 0; i < trs.length; i++) {
        if (col > trs[i].cells.length) {
            trs[i].deleteCell(trs[i].cells.length);
        }
        else {
            trs[i].deleteCell(col);
        }
    }
}

function insertitem() {
    var td = ControlSelectedParent();
    var tr = td.parentNode;
    tr.insertCell(td.cellIndex);
}

function deleteitem() {
    var td = ControlSelectedParent();
    var tr = td.parentNode;
    tr.deleteCell(td.cellIndex);
}