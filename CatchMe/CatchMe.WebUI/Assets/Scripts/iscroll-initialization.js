$(function () {
    var contentScroll = new IScroll('#container', {
        scrollbars: true,
        mouseWheel: true,
        interactiveScrollbars: true,
        shrinkScrollbars: 'scale',
        fadeScrollbars: true
    });

    setTimeout(function() {
        contentScroll.refresh();
    }, 2000);
    
    document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false);
});