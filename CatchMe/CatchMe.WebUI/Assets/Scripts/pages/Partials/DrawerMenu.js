$(function () {
    $(".mdl-navigation__link").click(function () {
        $('.mdl-layout__drawer, .mdl-layout__obfuscator').removeClass('is-visible');
    });
});