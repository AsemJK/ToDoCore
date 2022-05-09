//ToDo--------------------------------

function loadToDoListData(tenant)
{
    var apifullurl = "/Api/ToDo?tenant=" + tenant;
    $('#datatable_ToDoList').DataTable({

        "ajax": {
            url: apifullurl,
            "type": "GET",
            "Headers": { 'Id': '' },
            "dataSrc": function (response) {
                console.log(response);
                if (response.iTotalRecords == 0) {
                }
                return response;
            }
        }
        ,
        columns: [
            { data: "id" },
            { data: "toDoSubject" },
            { data: "teamMember" },
            { data: "creationDate" },
            { data: "lastStatus" },
            { data: "companyId" },
            {
                data: "id",
                "render": function (data, type, full, meta) { return '<a class="btn btn-round btn-info btn-icon btn-sm like" onclick="return viewService(' + full.Id + ');return false" title="View"><i class="fas fa-eye"></i></a><a class="btn btn-round btn-danger btn-icon btn-sm remove" href="#!" onclick="DeleteService(' + full.Id + ');return false;" title="Delete"><i class="fas fa-times"></i></a>'; }
            }
        ]
    });
}