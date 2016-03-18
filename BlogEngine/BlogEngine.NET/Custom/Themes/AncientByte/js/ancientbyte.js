$(document).ready(function () {
    $('#MainMenu .hasChildren').mouseover(function (e) {
        $('*[data-menu-child=' + $(this).data('menu-id') + ']').toggle();
        $(this).addClass('active');
    });

    $('#MainMenu .hasChildren').mouseout(function (e) {
        $('*[data-menu-child=' + $(this).data('menu-id') + ']').toggle();
        $(this).removeClass('active');
    });

    //Mobile click
    $('.mobileMenuTrigger').click(function () {
        $('#mobileMenuContent').toggle();
    });

    //Fix @ mobile click...
    $('#searchbox input').one("click", function () {
        $(this).val('');
        $(this).focus();
    });
});