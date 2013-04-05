$(document).ready(function(){
  $('#loginForm input[type=checkbox]').prettyCheckboxes();
});

$(document).ready(function () {
    $("a.poplight[href^=#]").click(function () {
        var popID = $(this).attr("rel");

        if (popID == 'loginForm')
            clearLoginForm();

        var popURL = $(this).attr("href");
        var query = popURL.split("?");
        var dim = query[1].split("&");
        var popWidth = dim[0].split("=")[1];
        $("#" + popID).fadeIn().css({
            width: Number(popWidth)
        }).prepend('<a href="#" class="close"></a>');
        var popMargTop = ($("#" + popID).height() + 80) / 2;
        var popMargLeft = ($("#" + popID).width() + 80) / 2;
        $("#" + popID).css({
            "margin-top": -popMargTop,
            "margin-left": -popMargLeft
        });
        $("body").append('<div id="fade"></div>');
        $("#fade").css({
            filter: "alpha(opacity=80)"
        }).fadeIn();
        return false
    });
    $("a.close, #fade").live("click", function () {
        $("#fade , .popup_block").fadeOut(function () {
            $("#fade, a.close").remove();
        });
        return false
    })
});