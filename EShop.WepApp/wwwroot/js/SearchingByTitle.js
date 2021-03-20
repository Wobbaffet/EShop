
//function FindBooksByTitle() {
//    if (event.keyCode === 13) {
//        console.log("enter")
//    }
//    else {
//        var selectedValue = $("#exampleDataList").val();
//        $.ajax({
//            url: '@Url.Action("FindBooksByTitle", "Book")',
//            data: { title: selectedValue },
//            method: "post",
//            success: function (data) {
//                _option = ``
//            },
//            error: function () {
//                alert("Error!");
//            }
//        });
//    }
//}



//function FindBooksByTitle() {

//    if (event.keyCode === 13) {
//        console.log("enter")
//    }
//    else {
//        var selectedValue = $("#exampleDataList").val();
//        callAjax(selectedValue);
//    }
//}


//function callAjax(selectedValue) {
//    var _option;
//    $.ajax({
//        url: $("#exampleDataList").data('request-url'),
//        data: { title: selectedValue },
//        method: "get",
//        success: function (data) {
//            console.log(data)
//            for (var i = 0; i < data.length; i++) {
//                _option = `<option>${data[i].title}</option>`;
//                $("#datalistOptions").append(_option);
//            }
//        },
//        error: function () {
//            alert("Error!");
//        }
//    });
//}