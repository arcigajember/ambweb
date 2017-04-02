function StudentGuardian() {
    $("#guardian-body #btn-remove-guardian")
        .click(function () {
            var row = $(this).closest("tr");
            var guardianId = row.find("#nr").text();
            var studentId = $("input[type=hidden]#Student_StudentId").val();
            debugger;

            if (confirm("Are you sure you want to remove this?")) {
                $.ajax({
                    type: "POST",
                    url: "Student/StudentGuardianDelete",
                    data: JSON.stringify({
                        studentId: studentId,
                        guardianId: guardianId
                    }),
                    contentType: "application/json",
                    dataType: "html",
                    cache: false,
                    success: function (data) {
                        $("#ajaxDetails").html(data);
                    },
                    error: function (xhr, data, status) {
                        alert(xhr);
                        alert(data);
                        alert(status);
                    }
                });
            }
        });
}