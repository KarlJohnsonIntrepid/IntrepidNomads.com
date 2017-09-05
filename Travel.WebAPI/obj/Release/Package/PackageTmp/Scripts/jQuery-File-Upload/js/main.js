/// <reference path="main.js" />
/*
 * jQuery File Upload Plugin JS Example
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

/* global $, window */

$(function () {
    'use strict';

    //Set defaut value of dropdown if passed via query string
    if (getParameterByName("blogId")) {
        $('#blogList').val(getParameterByName("blogId"));
    }

    setOptions();



    $('#blogList').change(function () {
        setOptions();
    });

    function setOptions() {
        // Initialize the jQuery File Upload widget:
        $('#fileupload').fileupload({
            // Uncomment the following to send cross-domain cookies:
            //xhrFields: {withCredentials: true},
            url: 'UploadImages' + queryString()
        });
    }

    function queryString() {
        var blogId = $('#blogList').val();
        if (blogId == "") {
            blogId = "0";
        }
        return '?BlogID=' + blogId;
    }

    function getParameterByName(name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    }

  
    // Enable iframe cross-domain access via redirect option:
    $('#fileupload').fileupload(
        'option',
        'redirect',
        window.location.href.replace(
            /\/[^\/]*$/,
            '/cors/result.html?%s'
        )
    );


    // Load existing files:
    $('#fileupload').addClass('fileupload-processing');
    $.ajax({
        // Uncomment the following to send cross-domain cookies:
        //xhrFields: {withCredentials: true},
        url: $('#fileupload').fileupload('option', 'url'),
        dataType: 'json',
        context: $('#fileupload')[0]
    }).always(function () {
        $(this).removeClass('fileupload-processing');
    }).done(function (result) {
        $(this).fileupload('option', 'done')
            .call(this, $.Event('done'), { result: result });
    });
    //}


    var $fileInput = $('#fileupload');

    $fileInput.on('fileuploaddone', function (e, data) {
        var activeUploads = $fileInput.fileupload('active');
        /*
         * activeUploads starts from the max number of files you are uploading to 1 when the last file has been uploaded.
         * All you have to do is doing a test on it value.
         */
        if (activeUploads == 1) {
            console.info("All uploads done");
            // Your stuff here
            $.get("api/image/clear", function () {});
        }
    });

});
