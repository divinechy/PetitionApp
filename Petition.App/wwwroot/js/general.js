
// Formating Money With Commas
function formatNumber(num, withOutNaira = false) {
    if (parseInt(num)) {
        if (withOutNaira) {
            return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
        } else {
            return '₦' + num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
        }

    }
    return num;
}

function ajaxAsync(Url, Data = {}, type = "", callback = function () { alert("Done"); }, isLoader = false) {
    $.ajax({ cache: false,
        url: Url,
        contentType: 'application/json; charset=UTF-8',
        type: type,
        crossDmain: true,
        headers: {
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, OPTIONS'
        },
        data: Data,
        beforeSend: function () {
            if (!isLoader)
                $(".loader_wrapper").fadeIn("fast");
        },
        success: function (data) {
            submitted = true;
            callback(data);
            //$('.btn').removeClass('hidden');

        },
        failure: function (msg) {

            alert('failure');

        },
        complete: function () {
            $(".loader_wrapper").fadeOut("slow");
        }

    });
}

function AddLoader(containerID) {
    var content = '';
    debugger;
    content += ' <span id="SpanLoader"><span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span><span class="sr-only">Loading...</span></span>';
    $('#' + containerID).append(content);
}

function RemoveLoader(containerID = null) {
    if (containerID) {
        $('#' + containerID).find('#SpanLoader').remove();
    }
    else {
        $('#SpanLoader').remove();
    }
}
function guid() {
    var gd = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0,
            v = c === 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
    return gd;
}
//function disableFraud(AskBe4Navigate = true, DisableEdit = true) {
//    debugger;
//    if (AskBe4Navigate) {
//        window.onbeforeunload = function () {
//            return "Are you sure you want to navigate away?";
//        };
//    }

    //if (DisableEdit) {
    //    document.addEventListener("contextmenu", function (e) {
    //        e.preventDefault();
    //    }, false);

    //    $(document).keydown(function (event) {
    //        if (event.keyCode == 123) { // Prevent F12
    //            return false;
    //        } else if (event.ctrlKey && event.shiftKey && event.keyCode == 73) { // Prevent Ctrl+Shift+I        
    //            return false;
    //        }
    //    });
    //}[]

//}