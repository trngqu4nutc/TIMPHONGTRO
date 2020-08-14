var homeConfig = {
    minP: 0,
    maxP: 100,
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
            home.filterPrice()
            homeConfig.html = ''
            homeConfig.pageIndex = 1
            home.loadData()
        })
    },
    filterPrice: function () {
        let choosePrice = parseInt($('.choose-price').val())
        switch (choosePrice) {
            case 1:
                homeConfig.minP = 0
                homeConfig.maxP = 1
                break
            case 2:
                homeConfig.minP = 1
                homeConfig.maxP = 2
                break
            case 3:
                homeConfig.minP = 2
                homeConfig.maxP = 3
                break
            case 4:
                homeConfig.minP = 3
                homeConfig.maxP = 5
                break
            case 5:
                homeConfig.minP = 5
                homeConfig.maxP = 7
                break
            case 6:
                homeConfig.minP = 7
                homeConfig.maxP = 10
                break
            case 7:
                homeConfig.minP = 10
                homeConfig.maxP = 100
                break
            default:
                homeConfig.minP = 0
                homeConfig.maxP = 100
                break
        }
    },
    loadData: function () {
        var model = {
            CategoryId: parseInt(homeConfig.type),
            ProvincialId: parseInt(homeConfig.tinh),
            DistrictId: parseInt(homeConfig.quan),
            StreetId: parseInt(homeConfig.duong),
            minP: homeConfig.minP,
            maxP: homeConfig.maxP,
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