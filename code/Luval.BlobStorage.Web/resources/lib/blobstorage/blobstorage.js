var bs = {
    init: function () {
        bs.initCopyHandle();
    },
    initCopyHandle: function () {
        $('[data-action]').on('click', function () {
            if ($(this).data('action') == "copy")
                bs.copy(this);
            else
                bs.delete(this);

        });
    },
    copy: function (el) {
        var url = $(el).data('url');
        clipboard.write(url);
        alert('File download url copied to the clipboard\n' + url);
    },
    delete: function (el) {
        var r = confirm('Are you sure you want to delete the file? ' + $(el).data('filename'));
        if (r != true) return;
        $.ajax({
            type: 'post',
            url: '/Media/Home/RemoveFile',
            data: { FullName: $(el).data('filename') },
            success: function (data, status, jqXHR) {
                if (jqXHR.status == "200") {
                    location.reload();
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                if (jqXHR.status == "200") {
                    location.reload();
                    return;
                }
                alert('Failed to delete the file ' + jqXHR.status + ' ' + jqXHR.statusText);
            },
            dataType: "json"
        });
    }
}

var clipboard = {
    write: function (text) {
        if (!navigator.clipboard) {
            clipboard.writeFailSafe(text);
            return;
        }
        navigator.clipboard.writeText(text).then(function () {
            console.log('Async: Copying to clipboard was successful!');
        }, function (err) {
            console.error('Async: Could not copy text: ', err);
        });
    },
    writeFailSafe: function (text) {
        var textArea = document.createElement("textarea");
        textArea.value = text;
        textArea.style.top = "0";
        textArea.style.left = "0";
        textArea.style.position = "fixed";

        document.body.appendChild(textArea);
        textArea.focus();
        textArea.select();

        try {
            var successful = document.execCommand('copy');
            var msg = successful ? 'successful' : 'unsuccessful';
            console.log('Fallback: Copying text command was ' + msg);
        } catch (err) {
            console.error('Fallback: Oops, unable to copy', err);
        }

        document.body.removeChild(textArea);
    }
}