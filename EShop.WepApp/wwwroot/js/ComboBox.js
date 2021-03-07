function customerChange() {

    var selectItem = document.getElementById("comboBox");

    var index = selectItem.selectedIndex;

    if (index == 0) {

        document.getElementById("div2").style.display = "none";
        document.getElementById("div1").style.display = "flex";

    }
    else {
        document.getElementById("div2").style.display = "flex";
        document.getElementById("div1").style.display = "none";

    }
}