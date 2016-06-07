var contType = {
    text: 1,
    pwd: 2,
    imgtxt: 3,
    check: 4,
    radio: 5,
    tref: 6,
    textmod: 7,
    date: 8,
    select: 9
};
//创建控件
function controls(fid, inno, label, tp, roly, selval) {
    var col = {
        field: fid ? fid : ""
        , indexNo: inno ? inno : 0
        , lbl: label ? label : ""
        , type: tp ? tp : 0
        , readonly: roly ? true : false
        , seldata: selval ? selval : ''
    };
    return col;
}
//获取form操作对象
function aceFrom(divid) {
    var aceFrom = {
        id: divid
        , cols: []
        , colsval: null
        , colindex: 2
        , loadErrorText: '数据加载出现异常'
    };
    var formexecJS = "";
    var controlGroup = '<div class="control-group">' +
                       '    <label class="control-label">[lbl]</label>' +
                       '    <div class="controls">' +
                       '        [control]' +
                       '       <span class="help-inline" id="help_[field]"></span>' +
                       '   </div>' +
                       '</div>';
    //初始化form 
    aceFrom.initCreate = function () {
        var fromHTML = this._initFrom();
        document.getElementById(this.id).innerHTML = fromHTML;
        eval(formexecJS);
    }
    //创建表单
    aceFrom._initFrom = function () {
        var fromhtm = '<fieldset><legend>基础数据</legend>';
        var issetfoot = false;
        for (var i = 0; i < this.cols.length; i++) {
            issetfoot = false;
            if (i % this.colindex == 0 && i != 0) {
                issetfoot = true;
                fromhtm += '</div>';
            }
            if (i % this.colindex == 0) {
                fromhtm += '<div class="row-fluid">';
            }
            fromhtm += '<div class="span' + parseInt(10 / this.colindex) + '">';
            fromhtm += controlGroup.replace("[lbl]", this.cols[i].lbl).replace("[control]", this._GetControlHTML(this.cols[i])).replace("[field]", this.cols[i].field);
            fromhtm += '</div>';
        }
        if (!issetfoot) {
            fromhtm += '</div></fieldset>';
        }
        return fromhtm;
    }
    aceFrom._GetControlHTML = function (col) {
        var retHTML = "";
        switch (col.type) {
            case contType.text:
                retHTML = '<input type="text" id="col_' + col.field + '" placeholder="' + col.lbl + '" />'
                ; break;
            case contType.pwd:
                retHTML = '<input type="password" id="col_' + col.field + '" placeholder="' + col.lbl + '" />'
                ; break;
            case contType.imgtxt:
                retHTML = '<span class="input-icon  input-icon-right">'
                        + '<input type="password" id="col_' + col.field + '" placeholder="' + col.lbl + '" />'
                        + '<i class="icon-edit"></i>'
                        + '</span>'
                ; break;
            case contType.check:
                if (col.seldata != null && col.seldata != '' && col.seldata.length != 0) {
                    var data = col.seldata.split('|');
                    for (var i = 0; i < data.length; i++) {
                        retHTML += '<label>'
                                    + '<input name="col_' + col.field + '" id="col_' + col.field + '_' + i + '" value="' + data[i] + '" type="checkbox" />'
                                    + '<span class="lbl">' + data[i] + '</span>'
                                    + '</label>';
                    }
                }
                ; break;
            case contType.radio:
                if (col.seldata != null && col.seldata != '' && col.seldata.length != 0) {
                    var data = col.seldata.split('|');
                    for (var i = 0; i < data.length; i++) {
                        retHTML += '<label>'
                                    + '<input name="col_' + col.field + '" id="col_' + col.field + '_' + i + '" value="' + data[i] + '" type="radio" />'
                                    + '<span class="lbl">' + data[i] + '</span>'
                                    + '</label>';
                    }
                }
                ; break;
            case contType.tref:
                retHTML = '<input  class="ace-switch  ace-switch-6" type="checkbox" id="col_' + col.field + '" /><span class="lbl"></span>';
                break;
            case contType.textmod:
                retHTML = ' <textarea class="span12" id="col_' + col.field + '" placeholder="' + col.lbl + '"></textarea>';
                ; break;
            case contType.date:
                retHTML = '  <span class="input-icon  input-icon-right">'
                + '<input id="col_' + col.field + '" placeholder="' + col.lbl + '" type="text" data-date-format="yyyy-mm-dd" />'
                + '<i class="icon-calendar"></i>'
                + '</span>'
                formexecJS += '$("#col_' + col.field + '").datepicker();';
                ; break;
            case contType.select:
                if (col.seldata != null && col.seldata != '' && col.seldata.length != 0) {
                    retHTML = '<select id="col_' + col.field + '">'
                    var data = col.seldata.split('|');
                    for (var i = 0; i < data.length; i++) {
                        retHTML += "<option value='" + data[i] + "' >" + data[i] + "</option>";
                    }
                    retHTML += "</select>";
                }
                ; break;
        }
        return retHTML;
    }
    //绑定数据
    aceFrom.DataBind = function () {
        if (this.colsval != null) {
            for (var i = 0; i < this.cols.length; i++) {
                var val = eval("this.colsval." + this.cols[i].field);
                if (val != undefined) {
                    if (this.cols[i].type == contType.text || this.cols[i].type == contType.imgtxt || this.cols[i].type == contType.pwd || this.cols[i].type == contType.textmod || this.cols[i].type == contType.date) {
                        document.getElementById("col_" + this.cols[i].field).value = val;
                    }
                    if (this.cols[i].type == contType.tref) {
                        document.getElementById("col_" + this.cols[i].field).checked = val;
                    }
                    if (this.cols[i].type == contType.check || this.cols[i].type == contType.radio) {
                        var data = this.cols[i].seldata.split('|');
                        for (var j = 0; j < data.length; j++) {
                            document.getElementById("col_" + this.cols[i].field + "_" + j).checked = false;
                        }
                        var vals = val.split('，');
                        for (var x = 0; x < vals.length; x++) {
                            for (var j = 0; j < data.length; j++) {
                                if (data[j] == vals[x]) {
                                    document.getElementById("col_" + this.cols[i].field + "_" + j).checked = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (this.cols[i].type == contType.select) {
                        var data = this.cols[i].seldata.split('|');
                        for (var j = 0; j < data.length; j++) {
                            if (data[j] == val) {
                                document.getElementById("col_" + this.cols[i].field).selectedIndex = j;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
    return aceFrom;
}