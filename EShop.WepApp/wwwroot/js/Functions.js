
$(document).ready(function () {
    $("#createMyAccount").click(function (event) {
        let x = 0;
        $("#signUp input").each(function () {
            if (
                this.classList.contains("is-invalid") ||
                !this.classList.contains("is-valid")
            ) {
                x++;
            }
        });
        console.log(x);
        if (x > 3) {
            event.preventDefault();
        }
    });
    var path = window.location.pathname;
    if (path == "/") {
        $("nav ul li:nth-child(1)").addClass("active");
    }
    else if (path.toLowerCase().includes("book")) {
        $("nav ul li:nth-child(2)").addClass("active");
    }
});


$(document).ready(function () {
    $("#signUp input").focusout(function () {
        console.log("Focusout");
        if ($(this).val() == "") {
            $(this).addClass("is-invalid");
            $(this).removeClass("is-valid");

            if ($(this).attr("name") == "Email") {
                $("#empty").show();
                $("#email").hide();
            }

            if ($(this).attr("name") == "Password") {
                $("#emptyPassword").show();
                $("#password").hide();
            }

            if ($(this).attr("name") == "ConfirmPassword") {
                $("#emptyConfirmPassword").show();
                $("#confirmPassword").hide();
            }
        }
    });

    $("#signUp input").keyup(function () {

        switch ($(this).attr("name")) {
            case "Email":

                RegexCheck("email", $("[name='Email']").val(), $("[name='Email']"));


                break;

            default:
                $(this).removeClass("is-invalid");
                $(this).addClass("is-valid");

                break;

            case "Password":


                ConfirmPassword();

                RegexCheck("password", $("[name='Password']").val(), $("[name='Password']"));


                break;

            case "ConfirmPassword":

                ConfirmPassword();

                break;
        }
    });
});


function RegexCheck(type, value, name) {

    var pattern;
    if (type == "email")
        pattern = /^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$/;
    else
        pattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{6,})/;

    if (value.match(pattern)) {
        name.removeClass("is-invalid");
        name.addClass("is-valid");
    } else {
        name.removeClass("is-valid");
        name.addClass("is-invalid");

        if (type == "password") {

            $("#emptyPassword").hide();
            $("#password").show();
        }
        else {
            $("#empty").hide();
            $("#email").show();
        }
    }

}

function ConfirmPassword() {
    var password = $("[name='Password']").val();

    var confirmPassword = $("[name='ConfirmPassword']").val();
    if (confirmPassword == password) {
        $("[name='ConfirmPassword']").removeClass("is-invalid");
        $("[name='ConfirmPassword']").addClass("is-valid");
    } else {
        $("[name='ConfirmPassword']").removeClass("is-valid");
        $("[name='ConfirmPassword']").addClass("is-invalid");

        $("#emptyConfirmPassword").hide();
        $("#confirmPassword").show();
    }
}
function ChangeComboBox() {
    var selectItem = document.getElementById("customerType");

    var index = selectItem.selectedIndex;

    if (index == 0) {
        document.getElementById("naturalPerson").removeAttribute("style");
        document.getElementById("legalEntity").style.display = "none";
    } else {
        document.getElementById("naturalPerson").style.display = "none";
        document.getElementById("legalEntity").removeAttribute("style");
    }
    $("input").val("");
    $("input").removeClass("is-valid");
    $("input").removeClass("is-invalid");
}


