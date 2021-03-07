for (var i = 0, j = 0; i <= 4; i++, j += 2)
    $('#loop').append(`
    <div class="row">
        <div class="list-group col-lg-3">
            <a href="javascript:void(0)" class="list-group-item active">
            <div class="content">
                                <img src="img/img${i + 1 + j}.jpg" alt="">
                                <div class="text">
                                    <h3>Tijamatin gnev</h3>
                                    <p>Džejms S. A. Kori</p>
                                </div>
                            </div>
                            <div class="description">
                                <div class="d-text">
                                    <h3>SUMMARY</h3>
                                    <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Magnam
                                        doloremque
                                        quo molestias. Numquam vitae commodi ipsam perferendis veniam,
                                        praesentium
                                        impedit.</p>
                                    <h4>DIRECTOR</h4>
                                    <p>John Doe</p>
                                    <h4>ACTIORS</h4>
                                    <p>John Doe</p>
                                    <p>John Doe</p>
                                    <p>John Doe</p>
                                    <p>John Doe</p>
                                </div>
                            </div>
            </a>
        </div>

        <div class="list-group col-lg-3">
            <a href="javascript:void(0)" class="list-group-item active">
            <div class="content">
                                <img src="img/img${i + 2 + j}.jpg" alt="">
                                <div class="text">
                                    <h3>Žirafe ne znaju da plešu</h3>
                                    <p>Džajls Andre</p>
                                </div>
                            </div>
                            <div class="description">
                                <div class="d-text">
                                    <h3>SUMMARY</h3>
                                    <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Magnam
                                        doloremque
                                        quo molestias. Numquam vitae commodi ipsam perferendis veniam,
                                        praesentium
                                        impedit.</p>
                                    <h4>DIRECTOR</h4>
                                    <p>John Doe</p>
                                    <h4>ACTIORS</h4>
                                    <p>John Doe</p>
                                    <p>John Doe</p>
                                    <p>John Doe</p>
                                    <p>John Doe</p>
                                </div>
                            </div>
            </a>
        </div>

        <div class="list-group col-lg-3">
            <a href="javascript:void(0)" class="list-group-item active">
            <div class="content">
                                <img src="img/img${i + 3 + j}.jpg" alt="">
                                <div class="text">
                                    <h3>Ljubavno pismo</h3>
                                    <p>Lusinda Rajli</p>
                                </div>
                            </div>
                            <div class="description">
                                <div class="d-text">
                                    <h3>SUMMARY</h3>
                                    <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Magnam
                                        doloremque
                                        quo molestias. Numquam vitae commodi ipsam perferendis veniam,
                                        praesentium
                                        impedit.</p>
                                    <h4>DIRECTOR</h4>
                                    <p>John Doe</p>
                                    <h4>ACTIORS</h4>
                                    <p>John Doe</p>
                                    <p>John Doe</p>
                                    <p>John Doe</p>
                                    <p>John Doe</p>
                                </div>
                            </div>
            </a>
        </div>
    </div>
`);


var numberOfItems = $("#loop .list-group").length;
var limitPerPage = 6;

$(`#loop .list-group:gt(${limitPerPage - 1})`).hide();
var totalPages = Math.round(numberOfItems / limitPerPage);

//inserted first item
$('.pagination').append(`<li class="page-item active"><a class="page-link" href="javascript:void(0)">1</a></li>`);
//loop throught other items
for (var i = 2; i <= totalPages; i++) {
    $('.pagination').append(`<li class="page-item"><a class="page-link" href="javascript:void(0)">${i}</a></li>`);
}
//insert right arrow
$('.pagination').append(`<li id='next-page' class="page-item-arrow"><a class="page-link" href="javascript:void(0)" aria-label="Next">    <span aria-hidden="true">&raquo;</span></a></li>`);

$('.pagination li.page-item').on("click", function () {
    if ($(this).hasClass("active"))
        return false;
    else {
        var currentPage = $(this).index();
        $('.pagination li').removeClass("active");
        $(this).addClass("active")
        $(`#loop .list-group`).hide();
        var grandTotal = limitPerPage * currentPage;

        for (var i = grandTotal - limitPerPage; i < grandTotal; i++) {
            $(`#loop .list-group:eq(${i})`).show();
        }
    }
});

$('#next-page').on("click", function () {
    var currentPage = $('.pagination li.active').index();
    if (currentPage === totalPages)
        return false;
    else {
        currentPage++;
        $('.pagination li').removeClass("active");
        $(`#loop .list-group`).hide();

        var grandTotal = limitPerPage * currentPage;

        for (var i = grandTotal - limitPerPage; i < grandTotal; i++) {
            $(`#loop .list-group:eq(${i})`).show();
        }

        $(`.pagination li.page-item:eq(${currentPage - 1})`).addClass("active");
    }
});

$('#previous-page').on("click", function () {
    var currentPage = $('.pagination li.active').index();
    if (currentPage === 1)
        return false;
    else {
        currentPage--;
        $('.pagination li').removeClass("active");
        $(`#loop .list-group`).hide();

        var grandTotal = limitPerPage * currentPage;

        for (var i = grandTotal - limitPerPage; i < grandTotal; i++) {
            $(`#loop .list-group:eq(${i})`).show();
        }

        $(`.pagination li.page-item:eq(${currentPage - 1})`).addClass("active");
    }
});