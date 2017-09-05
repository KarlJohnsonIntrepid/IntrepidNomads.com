function loadTinyMCE(css1, css2) {

    tinymce.init({
        selector: "textarea",
        plugins: 'link image code paste',
        convert_urls: false,
        content_css: css1 + "," + css2,
        paste_as_text: true,
        paste_text_sticky: true,
        image_advtab: true
    });

}

function loadDotDotDot(selector) {
    $(selector).dotdotdot({
        ellipsis: '... ',
        wrap: 'word',
        after: "a.readmore",
        height: 180,
        tolerance: 0,
    });
}


function copy(clickControl, controlToCopyFrom) {
    clickControl.click(function () {
        copyToClipboard(controlToCopyFrom.val());
    });
}

function copyToClipboard(text) {
    window.prompt("Copy to clipboard: Ctrl+C, Enter", text);
}

function loadFancyBox() {

    $(".fancybox").fancybox({
        helpers: {
            title: {
                type: 'over'
            }
        }
    });
}