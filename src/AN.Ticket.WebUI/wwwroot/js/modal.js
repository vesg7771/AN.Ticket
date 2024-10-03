const editModal = document.getElementById('editPlanoPagamentoModal');
editModal.addEventListener('show.bs.modal', (event) => {
    const button = event.relatedTarget; // Botão que acionou o modal
    const id = button.getAttribute('data-id');
    const description = button.getAttribute('data-description');
    const value = button.getAttribute('data-value');

    // Preencher os campos do modal com os dados do item
    const modalBody = editModal.querySelector('.modal-body');
    modalBody.querySelector('input[name="Id"]').value = id;
    modalBody.querySelector('input[name="Description"]').value = description;
    modalBody.querySelector('input[name="Value"]').value = value;
});