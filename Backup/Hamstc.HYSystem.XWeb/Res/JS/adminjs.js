function ExePostBack(objId, objmsg) {
    var msg = "删除记录后不可恢复，您确定吗？";
    if (arguments.length == 2) {
        msg = objmsg;
    }
    parent.dialog({
        title: '提示',
        content: msg,
        okValue: '确定',
        ok: function () {
            __doPostBack(objId, '');
        },
        cancelValue: '取消',
        cancel: function () { }
    }).showModal();
    return false;
} 