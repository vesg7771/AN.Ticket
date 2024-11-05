$(document).ready(function () {
    $('.create-team-button').click(function () {
        openCreateTeamModal();
    });

    function openCreateTeamModal() {
        $.get('/Team/CreateTeamModal', function (data) {
            $('#modalContainer').html(data);
            $('#createTeamModal').modal('show');
        });
    }

    window.searchMembers = function (teamId) {
        const searchTerm = document.getElementById(`searchTerm-${teamId}`).value;
        fetch(`/Team/GetPagedTeamMembers?teamId=${teamId}&pageNumber=1&pageSize=10&searchTerm=${encodeURIComponent(searchTerm)}`)
            .then(response => response.text())
            .then(html => {
                document.getElementById(`teamMembersTable-${teamId}`).innerHTML = html;
            });
    };

    $(document).on('submit', '#createTeamForm', function (e) {
        e.preventDefault();
        var form = $(this);
        $.ajax({
            url: '/Team/CreateTeam',
            type: 'POST',
            data: form.serialize(),
            success: function (result) {
                if (result.success) {
                    $('#createTeamModal').modal('hide');
                    location.reload();
                } else {
                    toastr.error(result.errors.join("\n"));
                }
            }
        });
    });
});