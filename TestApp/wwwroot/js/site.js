

function orderChange() {
    var one = $("#orderingSelectBoxId").val();
    var search = $("#searchInput").val();
    $.ajax({
        url: '/Home/OrderChange',
        type: 'post',
        asynch: false,
        data: {
            name: one,
            search : search
        },
        success: function (result) {
            $("#tableIdMethod").html(result);

        },
        error: function (result) {
            if (this.value != this.search) {
                alert("error");
            }

            //_loginModal.appendMessage(result, 1);
            _generalMethods.endLoadingOnButton("#login-continue-btnId");
        }

    });
}




function addNew() {
     $("#personsFormId").attr("action", "/Home/CreatPerson");
}


$(document).ready(function () {
    $("#exampleModal").on("hidden.bs.modal", function () {
        $("#personsFormId input").val("");
    });
});


function EditPerson(e) {
    $("#personsFormId").attr("action", "/Home/UpdatePerson");

    var id = parseInt($(e).attr("data-edited"));
    if (id) {
        $.ajax({
            url: '/Home/EditPerson',
            type: 'post',
            asynch: false,
            data: { id: id },
            success: function (result) {
                if (result.person) {
                    $("[name=Id]").val(result.person.id);
                    $("[name=Name]").val(result.person.name);
                    $("[name=Surname]").val(result.person.surname);
                    $("[name=Birthdate]").val(result.person.birthdate);
                    $("[name=Website]").val(result.person.website);
                    $(["name=Country"]).val(result.person.country);
                    $("[name=Pin]").val(result.person.pin);
                    $("[name=Mobile]").val(result.person.mobile);
                    $("[name=Email]").val(result.person.email);
                    $("[name=GenderId]").val(result.person.genderid);
                } else {
                    _notify.show("მონაცემები არ მოიპოვება", "error", 3000)
                }
            },
            error: function (result) {
                _notify.show(result, "error", 4000);

                //_loginModal.appendMessage(result, 1);
                _generalMethods.endLoadingOnButton("#login-continue-btnId");
            }
        });
    }
}


function deleteMethod(e) {
    var id = parseInt($(e).attr("data-deleteId"));
    if (id) {
        $.ajax({
            url: '/Home/DeletePerson',
            type: 'post',
            asynch: false,
            data: { id: id },
            success: function (result) {
                if (result.status) {
                    _notify.show("მონაცემები წარმატებით წაიშალა","success",3000)
                    var row = $("#personTableId tr[data-rowId=" + id + "]");
                    row.detach();
                    //location.reload();
                } else {
                    _notify.show("წაშლა ვერ მოხერხდა", "error", 3000)
                }
            },
            error: function (result) {
                _notify.show(result, "error", 4000);

                //_loginModal.appendMessage(result, 1);
                _generalMethods.endLoadingOnButton("#login-continue-btnId");
            }
        });
    }
}

let _notify = {
    element: undefined,
    hideBtn: {
        btn: undefined,
        onClick: function () {
            _notify.hide();
        }
    },
    setTimeOut: undefined,
    setText: function (text, type) {
        if (type === "success") {
            this.element.css("background-color", "rgba(85, 190, 85, 0.92)");
        }
        else {
            this.element.css("background-color", "rgba(255, 70, 70, 0.92)");
        }
        this.element.children("span").text(text);
    },
    show: function (text, type, time) {
        if (this.setTimeOut) {
            clearTimeout(this.setTimeOut);
        }
        this.element.hide();
        this.setText(text, type);

        this.element.slideDown(300);
        this.setTimeOut = setTimeout(function () {
            _notify.element.slideUp(300);
        }, time);
    },
    hide: function () {
        clearTimeout(this.setTimeOut);
        this.element.slideUp(300);
    },
    init: function (element, hideBtn) {
        this.element = $(element);
        this.hideBtn.btn = $(hideBtn);
        this.hideBtn.btn.on("click", function () { _notify.hideBtn.onClick(); });
    }
};