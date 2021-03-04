
function customerChange() {

    var selectItem = document.getElementById("comboBox");

    var index = selectItem.selectedIndex;

    if (index == 0) {

        document.getElementById("div2").style.visibility = "hidden";
        document.getElementById("div1").style.visibility = "visible"

    }
    else {
        document.getElementById("div2").style.visibility = "visible";
        document.getElementById("div1").style.visibility = "hidden";

    }
}