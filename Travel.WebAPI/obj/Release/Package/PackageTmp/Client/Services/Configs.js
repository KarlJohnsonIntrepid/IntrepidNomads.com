nomadsApp.factory("tinymceOptions", function () {

    'use strict';

    $(document).on('focusin', function(e) {
        if ($(e.target).closest(".mce-window").length) {
            e.stopImmediatePropagation();
        }
    });

    return {
        document_base_url: "/content/tinymce",
        baseURL: "/content/tinymce",
        plugins: 'link image code paste',
        convert_urls: false,
        content_css: '/Content/bootstrap.min.css,/Content/Site.css',
        paste_as_text: true,
        paste_text_sticky: true,
        image_advtab: true
    };
});