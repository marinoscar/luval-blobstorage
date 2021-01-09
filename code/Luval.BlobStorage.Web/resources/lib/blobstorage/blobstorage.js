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
        $('#url-copy').val(url);
        var copyText = document.getElementById("url-copy");
        copyText.select();
        document.execCommand("copy");
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