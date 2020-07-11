var homeConfig = {
    type: 1,
    tinh: 1,
    quan: 0,
    duong: 0,
    pageSize: 2,
    pageIndex: 1,
    html: ''
}

var home = {
    init: function () {
        home.registerEvent()
        home.loadData()
    },
    registerEvent: function () {
        $('#btnLoc').off('click').on('click', () => {
            homeConfig.type = $('#search_room_type').val()
            homeConfig.tinh = $('#search_city').val()
            homeConfig.quan = $('#tblDataQuan').val()
            homeConfig.duong = $('#tblDataDuong').val()
            homeConfig.html = ''
            homeConfig.pageIndex = 1
            home.loadData()
        })
    },
    loadData: function () {
        var model = {
            CategoryId: parseInt(homeConfig.type),
            ProvincialId: parseInt(homeConfig.tinh),
            DistrictId: parseInt(homeConfig.quan),
            StreetId: parseInt(homeConfig.duong),
            Price: 0,
            Area: '0'
        }
        $.ajax({
            url: '/Home/LoadData',
            type: 'GET',
            data: {
                model: JSON.stringify(model),
                page: homeConfig.pageIndex,
                pageSize: homeConfig.pageSize
            },
            dataType: 'json',
            success: res => {
                var data = res.data
                var template = $('#data-template').html()
                if (res.total > 0) {
                    $.each(data, (i, item) => {
                        homeConfig.html += Mustache.render(template, {
                            Picture: item.Picture,
                            Title: item.Title,
                            SortContent: item.SortContent,
                            Id: item.NewsId,
                            StartDate: home.formatDate(item.StartDate),
                            Fullname: item.Fullname
                        })
                        let loadMore = $('#data-btnLoadMore').html()
                        let htmlLoadMore = ''
                        htmlLoadMore += Mustache.render(loadMore, {
                            soluong: (res.total - 1)
                        })
                        $('#product-pagination').html(htmlLoadMore)
                        $('#tblData').html(homeConfig.html)
                        if (res.total <= 1) {
                            $('#btnXemThem').hide()
                        } else {
                            $('#btnXemThem').show()
                        }
                        $('#btnXemThem').off('click').on('click', () => {
                            homeConfig.pageIndex = homeConfig.pageIndex + 1
                            setTimeout(home.loadData, 200)
                        })
                    })
                } else {
                    $('#tblData').html(homeConfig.html)
                }
            },
            error: e => {
                console.log('error')
            }
        })
    },
    formatDate: (jsonDate) => {
        var date = new Date(parseInt(jsonDate.substr(6)))
        return (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear()
    }
}



home.init()