<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAdminJS.ascx.cs" Inherits="Hamstc.HYSystem.XWeb.UControl.UCAdminJS" %>
<script type="text/javascript" charset="utf-8" src="../Res/JS/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../Res/JS/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../Res/JS/datepicker/WdatePicker.js"></script>
<script type="text/javascript" charset="utf-8" src="../Res/JS/jquery/jquery.nicescroll.js"></script>
<script type="text/javascript" charset="utf-8" src="../Res/JS/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../Res/JS/layindex.js"></script>
<script type="text/javascript" charset="utf-8" src="../Res/JS/common.js"></script>
<script type="text/javascript">
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
</script>