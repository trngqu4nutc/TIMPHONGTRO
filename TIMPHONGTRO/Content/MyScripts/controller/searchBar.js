var search = {
    init: function () {
        search.registerEvent()
        //search.logout()
    },
    registerEvent: function () {
        $.ajax({
            url: '/Home/LoadQuan',
            type: 'GET',
            dataType: 'json',
            success: res => {
                var data = res.data
                var html = `<option value="0">
                                -- Quận/huyện --
                            </option>`
                var template = $('#data-template-quan').html()
                $.each(data, function (i, item) {
                    html += Mustache.render(template, {
                        valquan: item.DistrictId,
                        namequan: item.Name
                    })
                })
                $('#tblDataQuan').html(html)
            },
            error: e => {
                console.log('error')
            }
        })

        $.ajax({
            url: '/Home/LoadLoai',
            type: 'GET',
            dataType: 'json',
            success: res => {
                var data = res.data
                var html = ``
                var template = $('#data-template-loai').html()
                $.each(data, function (i, item) {
                    html += Mustache.render(template, {
                        valloai: item.CategoryId,
                        nameloai: item.Description
                    })
                })
                $('#search_room_type').html(html)
            },
            error: e => {
                console.log('error')
            }
        })

        $('#tblDataQuan').change(function () {
            let value = $('#tblDataQuan').val()
            $.ajax({
                url: '/Home/LoadDuong',
                type: 'GET',
                data: { DistrictId: value },
                dataType: 'json',
                success: res => {
                    var data = res.data
                    var html = `<option value="0">
                                Chọn đường phố
                            </option>`
                    var template = $('#data-template-duong').html()
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            valduong: item.StreetId,
                            nameduong: item.Name
                        })
                    })
                    $('#tblDataDuong').html(html)
                },
                error: e => {
                    console.log('error')
                }
            })
        })

    },
    /*logout: function () {
        $.ajax({
            url: '/login/logout',
            type: 'POST',
            dataType: 'json',
            success: res => {
                console.log(res.msg)
                window.location.reload()
            },
            error: e => {
                console.log(e)
            }
        })
    }*/
}

search.init()