function pageSession() {
    var _index = 0;
    this.index = new Array();
    this.keys = new Array();
    this.data = new Array();

    this.getPageset = function (key) {
        var value = ___getpageset();
        if (this.data[key] == null) {
            this.keys.push(value);
            this.data[key] = value;
        }
        return this.data[key];
    }

    this.count = function () {
        return this.keys.length;
    };

    this.___getpageset = function () {
        return {
            pageid: null,
            listpage: {
                _listset: null,
                listset: function () {
                    if (this.pageId != null && _listset != null) {

                    }
                    return _listset;
                },
                databind: function () {

                },
                pageindex: 0,
                pagesize: 0,
                pagecount: 0
            },
            editpage: {
                _editlist: null,
                editlist: function () {
                    if (this.pageId != null && _editlist != null) {

                    }
                    return _editlist;
                }
            },
            getbind: function () {

            }
        };
    }
}
var ps = new pageSession();
function myfunction() {
    var s = ps.getPageset("str");
    
}