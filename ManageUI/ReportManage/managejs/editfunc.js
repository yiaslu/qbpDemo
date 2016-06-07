var gSetColorType = "";
var gIsIE = document.all;
var gIEVer = fGetIEVer();
var gLoaded = false;
var ev = null;
var gSigns = "";
var gSignHtml = "";


function fGetEv(e) {
    ev = e;
}

function ResetDomain() {
    var f = window.frames["frameReport"];
    var ss = document.domain; 					//ss == "www.moransoft.cn";
    var ii = ss.lastIndexOf('.');
    if (ii > 0) {
        if (!isNaN(ss.substr(ii + 1) * 1))
            return;
        ii = ss.lastIndexOf('.', ii - 1);
        if (ii > 0) {
            f.document.domain = ss.substr(ii + 1);
            document.domain = ss.substr(ii + 1);
        }
    } 										//document.domain == "moransoft.cn";
}

function fGetIEVer() {
    var iVerNo = 0;
    var sVer = navigator.userAgent;
    if (sVer.indexOf("MSIE") > -1) {
        var sVerNo = sVer.split(";")[1];
        sVerNo = sVerNo.replace("MSIE", "");
        iVerNo = parseFloat(sVerNo);
    }
    return iVerNo;
}

function fSetEditable() {
    var f = window.frames["frameReport"];
    f.document.designMode = "on";
    if (!gIsIE)
        f.document.execCommand("useCSS", false, true);
}

function fSetFrmClick() {
    var f = window.frames["frameReport"];
    f.document.onclick = function () {
        fHideMenu();
    }
    f.document.onkeydown = function () {
        //parent.gIsEdited = true;
    }
}

//window.onload = function(){
//	gLoaded = true;
//	fSetEditable();
//	fSetFrmClick();
//}

function fSetColor() {
    var dvForeColor = document.getElementById("dvForeColor");
    if (dvForeColor.getElementsByTagName("TABLE").length == 1) {
        dvForeColor.innerHTML = drawCube() + dvForeColor.innerHTML;
    }
}

document.onmousemove = function (e) {
    if (gIsIE) var el = event.srcElement;
    else var el = e.target;
    var tdView = document.getElementById("tdView");
    var tdColorCode = document.getElementById("tdColorCode");

    if (el.tagName == "IMG") {
        try {
            if (fInObj(el, "dvForeColor")) {
                tdView.bgColor = el.parentNode.bgColor;
                tdColorCode.innerHTML = el.parentNode.bgColor
            }
        } catch (e) { }
    }
}

function fInObj(el, id) {
    if (el) {
        if (el.id == id) {
            return true;
        } else {
            if (el.parentNode) {
                return fInObj(el.parentNode, id);
            } else {
                return false;
            }
        }
    }
}

function fDisplayObj(id) {
    var o = document.getElementById(id);
    if (o) o.style.display = "";
}

document.onclick = function (e) {
    if (gIsIE) var el = event.srcElement;
    else var el = e.target;
    var dvForeColor = document.getElementById("dvForeColor");
    var dvPortrait = document.getElementById("dvPortrait");

    if (el.tagName == "IMG") {
        try {
            if (fInObj(el, "dvForeColor")) {
                format(gSetColorType, el.parentNode.bgColor);
                dvForeColor.style.display = "none";
                return;
            }
        } catch (e) { }
    }
    fHideMenu();
    var hideId = "";
    if (arrMatch[el.id]) {
        hideId = arrMatch[el.id];
        fDisplayObj(hideId);
    }
}
var arrMatch = {
    imgFontface: "fontface",
    imgFontsize: "fontsize",
    imgFontColor: "dvForeColor",
    imgBackColor: "dvForeColor",
    imgAlign: "divAlign",
    imgList: "divList",
    imgInOut: "divInOut"
}

function format(type, para) {
    var f = window.frames["frameReport"];
    var sAlert = "";
    if (!gIsIE) {

    }
    if (sAlert != "") {
        alert(sAlert);
        return;
    }
    f.focus();
    if (!para) {
        if (gIsIE) {
            f.document.execCommand(type);
        } else {
            f.document.execCommand(type, false, false);
        }
    } else {
        f.document.execCommand(type, false, para);
    }
    f.focus();
}

function setMode(bStatus) {
    var sourceEditor = document.getElementById("sourceEditor");
    var frameReport = document.getElementById("frameReport");
    //var divEditor = document.getElementById("divEditor");
    var f = window.frames["frameReport"];
    var body = f.document.getElementsByTagName("BODY")[0];


    if (bStatus) {
        //		sourceEditor.style.display = "";
        //		frameReport.style.display="none";
        //divEditor.style.display = "none";
        sourceEditor.value = body.innerHTML;
        document.getElementById("tr1").style.display = "none";
        document.getElementById("tr2").style.display = "";
        //		frameReport.style.height="0";
        //		sourceEditor.style.height="100%";
    } else {
        //		sourceEditor.style.display = "none";
        //		frameReport.style.display="";
        //divEditor.style.display = "";
        body.innerHTML = sourceEditor.value;
        document.getElementById("tr1").style.display = "";
        document.getElementById("tr2").style.display = "none";
        //		frameReport.style.height="100%";
        //		sourceEditor.style.height="0";
    }

}

function foreColor(e) {
    fDisplayColorBoard(e);
    gSetColorType = "foreColor";
}

function fDisplayColorBoard(e) {

    if (gIsIE) {
        var e = window.event;
    }
    if (gIEVer <= 5.01 && gIsIE) {
        var arr = showModalDialog("ColorSelect.htm", "", "font-family:Verdana; font-size:12; status:no; dialogWidth:21em; dialogHeight:21em");
        if (arr != null) return arr;
        return;
    }
    var dvForeColor = document.getElementById("dvForeColor");
    // fSetColor();
    var iX = e.clientX;
    var iY = e.clientY;
    dvForeColor.style.display = "";
    dvForeColor.style.left = (iX - 30) + "px";
    dvForeColor.style.top = 33 + "px";
    // EV.stopEvent();
    return true;
}

function createLink() {
    var sURL = window.prompt("请输入链接 (如:http://www.baidu.com):", "http://");
    if ((sURL != null) && (sURL != "http://")) {
        format("CreateLink", sURL);
    }
}

function createImg() {
    var sPhoto = prompt("请输入图片位置:", "http://");
    if ((sPhoto != null) && (sPhoto != "http://")) {
        format("InsertImage", sPhoto);
    }
}

String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}

function fDisplayElement(element, displayValue) {
    if (gIEVer <= 5.01 && gIsIE) {
        if (element == "fontface") {
            var sReturnValue = showModalDialog("FontFaceSelect.htm", "", "font-family:Verdana; font-size:12; status:no; unadorned:yes; scroll:no; resizable:yes;dialogWidth:112px; dialogHeight:271px"); ;
            format("fontname", sReturnValue);
        } else if (element == "fontsize") {
            var sReturnValue = showModalDialog("FontSizeSelect.htm", "", "font-family:Verdana; font-size:12; status:no; unadorned:yes; scroll:no; resizable:yes;dialogWidth:130px; dialogHeight:250px"); ;
            format("fontsize", sReturnValue);
        } else if (element == "divAlign") {
            var sReturnValue = showModalDialog("AlignSelect.htm", "", "font-family:Verdana; font-size:12; status:no; unadorned:yes; scroll:no; resizable:yes;dialogWidth:40px; dialogHeight:45px"); ;
            format(sReturnValue);
        } else if (element == "divList") {
            var sReturnValue = showModalDialog("ListSelect.htm", "", "font-family:Verdana; font-size:12; status:no; unadorned:yes; scroll:no; resizable:yes;dialogWidth:60px; dialogHeight:45px"); ;
            format(sReturnValue);
        } else if (element == "divInOut") {
            var sReturnValue = showModalDialog("InOutdent.htm", "", "font-family:Verdana; font-size:12; status:no; unadorned:yes; scroll:no; resizable:yes;dialogWidth:60px; dialogHeight:45px"); ;
            format(sReturnValue);
        }

        return;
    }
    fHideMenu();
    if (typeof element == "string")
        element = document.getElementById(element);
    if (element == null) return;
    element.style.display = displayValue;
    if (gIsIE) {
        var e = event;
        var target = e.srcElement;
    } else {
        var e = ev;
        var target = e.target;
    }
    var iX = f_GetX(target);
    element.style.display = "";
    element.style.left = (iX) + "px";
    element.style.top = 33 + "px";
    // EV.stopEvent();
    return true;
}

function fSetModeTip(obj) {
    var x = f_GetX(obj);
    var y = f_GetY(obj);
    var dvModeTip = document.getElementById("dvModeTip");
    if (!dvModeTip) {
        var dv = document.createElement("DIV");
        dv.style.position = "absolute";
        dv.style.top = 33 + "px";
        dv.style.left = (x - 40) + "px";
        dv.style.zIndex = "999";
        dv.style.fontSize = "12px";
        dv.id = "dvModeTip";
        dv.style.padding = "2 2 0 2px";
        dv.style.border = "1px #000000 solid";
        dv.style.backgroundColor = "#FFFFCC";
        dv.style.height = "12px";
        dv.innerHTML = "编辑源码";
        document.body.appendChild(dv);
    } else {
        dvModeTip.style.display = "";
    }
}

function fHideTip() {
    document.getElementById("dvModeTip").style.display = "none";
}

function f_GetX(e) {
    var l = e.offsetLeft;
    while (e = e.offsetParent) {
        l += e.offsetLeft;
    }
    return l;
}

function f_GetY(e) {
    var t = e.offsetTop;
    while (e = e.offsetParent) {
        t += e.offsetTop;
    }
    return t;
}

function fHideMenu() {
    try {
        var arr = ["fontface", "fontsize", "dvForeColor", "divAlign", "divList", "divInOut"];
        for (var i = 0; i < arr.length; i++) {
            var obj = document.getElementById(arr[i]);
            if (obj) {
                obj.style.display = "none";
            }
        }
    } catch (exp) { }
}

function fHide(obj) {
    obj.style.display = "none";
}
gIsHtml = true;

String.prototype.stripTags = function () {
    return this.replace(/<\/?[^>]+>/gi, '');
};

String.prototype.unescapeHTML = function () {
    var div = document.createElement('div');
    div.innerHTML = this.stripTags();
    return div.childNodes[0].nodeValue;
};

// Draw color selector
// create 6-element array
var s = "";
var hex = new Array(6)
// assign non-dithered descriptors
hex[0] = "FF"
hex[1] = "CC"
hex[2] = "99"
hex[3] = "66"
hex[4] = "33"
hex[5] = "00"
// draw a single table cell based on all descriptors
function drawCell(red, green, blue) {
    // open cell with specified hexadecimal triplet background color
    var color = '#' + red + green + blue;
    if (color == "#000066") color = "#000000";
    s += '<TD BGCOLOR="' + color + '" style="height:12px;width:12px;" >';
    // print transparent image (use any height and width)
    s += '<IMG ' + ((document.all) ? "" : "src='place.gif'") + ' HEIGHT=12 WIDTH=12>';
    // close table cell
    s += '</TD>';
}
// draw table row based on red and blue descriptors
function drawRow(red, blue) {
    // open table row
    s += '<TR>';

    // loop through all non-dithered color descripters as green hex
    for (var i = 0; i < 6; ++i) {
        drawCell(red, hex[i], blue)
    }
    // close current table row
    s += '</TR>';
}
// draw table for one of six color cube panels
function drawTable(blue) {
    // open table (one of six cube panels)
    s += '<TABLE CELLPADDING=0 CELLSPACING=0 BORDER=0>';
    // loop through all non-dithered color descripters as red hex
    for (var i = 0; i < 6; ++i) {
        drawRow(hex[i], blue)
    }
    // close current table
    s += '</TABLE>';
}
// draw all cube panels inside table cells
function drawCube() {
    // open table
    s += '<TABLE CELLPADDING=0 CELLSPACING=0 style="border:1px #888888 solid"><TR>';
    // loop through all non-dithered color descripters as blue hex
    for (var i = 0; i < 2; ++i) {
        // open table cell with white background color
        s += '<TD BGCOLOR="#FFFFFF">';
        // call function to create cube panel with hex[i] blue hex
        drawTable(hex[i])
        // close current table cell
        s += '</TD>';
    }
    s += '</TR><TR>';
    for (var i = 2; i < 4; ++i) {
        // open table cell with white background color
        s += '<TD BGCOLOR="#FFFFFF">';
        // call function to create cube panel with hex[i] blue hex
        drawTable(hex[i])
        // close current table cell
        s += '</TD>';
    }
    // close table row and table
    s += '</TR></TABLE>';
    return s;
}

function fontname(obj) { format('fontname', obj.innerHTML); obj.parentNode.style.display = 'none' }
function fontsize(size, obj) { format('fontsize', size); obj.parentNode.style.display = 'none' }