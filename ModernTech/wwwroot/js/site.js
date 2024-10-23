function GetStudents() {
    fetch("../Student/Students", {
        method: "GET",
    })
    .then(response => response.json())
        .then(Students => {
            PrintStudents(Students);
    });
}
 function PrintStudents(Students){
     var page = document.querySelector("#list_of_students tbody");
     page.innerHTML = '';
     for (i = 0; i < Students.length; i++) {
         page.innerHTML +=
             '<tr>'
             +
             '<td> <input id="delcheck' + Students[i].id + '" class="custom-checkbox mini" type="checkbox" value="' + Students[i].id + '"/> <label for="delcheck' + Students[i].id + '"></label> </td>'
             +
             '<td>' + Students[i].id + '</td>'
             +
             '<td>' + Students[i].fullName + '</td>'
             +
             '<td>' + Students[i].numberOfRecordBook + '</td>'
             +
             '<td>' + Students[i].birthDate + '</td>'
             +
             '<td>' + Students[i].enrollementDate + '</td>'
             +
             '</tr>';
     }
}
function PostStudent() {
    var Create = document.querySelectorAll(".C input");
    if (Create[0].value === "" || Create[1].value === "" || Create[2].value === "" || Create[3].value === "") {
        alert("Все поля должны быть заполнены");
        return;
    }
    var jsondata = {
        FullName: Create[0].value,
        NumberOfRecordBook: Create[1].value,
        BirthDate: Create[2].value,
        EnrollementDate: Create[3].value
    };
    fetch("../Student/Student", {
        method: "POST",
        body: JSON.stringify(jsondata),
        headers: { "Content-Type": "application/json" }
    })
    .then(response => response.json())
    .then(Response => {
        switch(Response){
            case 1: {
                GetStudents();
                break;
            }
            case 0: {
                alert("Ошибка вставки данных, проверьте поля ввода");
                break;
            }
        }
    });
}
function CheckAll(maininput) {
    if(maininput.checked)
        document.querySelectorAll('#list_of_students tbody input[type="checkbox"]').forEach(element => {
            element.checked = true;
        });
    else
        document.querySelectorAll('#list_of_students tbody input[type="checkbox"]').forEach(element => {
            element.checked = false;
        });
}
function DeleteStudent() {
    var Delete = document.querySelectorAll('#list_of_students tbody input[type="checkbox"]:checked');
    if (Delete.length == 0) {
        alert("Вы не выбрали объект(ы) удаления");
        return;
    }
    var jsondata = [];
    Delete.forEach(element => {
        jsondata.push(Number(element.value));
    });
    fetch("../Student/Student", {
        method: "DELETE",
        body: JSON.stringify(jsondata),
        headers: { "Content-Type": "application/json" }
    })
    .then(response => response.json())
    .then(Response => {
        switch (Response) {
            case 1: {
                GetStudents();
                break;
            }
            case 0: {
                alert("Ошибка удаления данных");
                break;
            }
        }
    });
}
function Filter() {
    var Filter = document.querySelectorAll('.F input');
    jsondata = {
        enrollfrom: Filter[0].value,
        enrollto: Filter[1].value,
        agefrom: Filter[2].value == "" ? 0 : Filter[2].value,
        ageto: Filter[3].value == "" ? 0 : Filter[3].value,
    };
    fetch("../Student/FilterStudent", {
        method: "POST",
        body: JSON.stringify(jsondata),
        headers: { "Content-Type": "application/json" }
    })
    .then(response => response.json())
    .then(Students => {
            PrintStudents(Students);
    });
}
function ResetFilter() {
    var Filter = document.querySelectorAll('.F input');
    Filter.forEach(element => {
        element.value = null;
    });
    GetStudents();
}