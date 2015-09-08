$(document).ready(function () {    
    $(".exclusao").click(function () { return confirmaExclusao(); });
    $("#incluiFuncionario").click(function () { return confirmaInclusaoFuncionario(); });
    $("#alteraFuncionario").click(function () { return confirmaAlteracaoFuncionario(); });
    $(".excluiFuncionario").click(function () { return confirmaExclusaoFuncionario(); });
    $("#incluiQuestionario").click(function () { return confirmaInclusaoQuestionario(); });
    $("#alteraQuestionario").click(function () { return confirmaAlteracaoQuestionario(); });
    $(".excluiQuestionario").click(function () { return confirmaAlteracaoQuestionario(); });
    $("#incluiQuestao").click(function () { return confirmaInclusaoQuestao(); });
    $("#alteraQuestao").click(function () { return confirmaAlteracaoQuestao(); });
    $(".excluiQuestao").click(function () { return confirmaExclusaoQuestao(); });
    $("#marcar").click(function () { return marcar(); });
    $("#desmarcar").click(function () { return desmarcar(); });
    $("#btnPasso3").click(function () { return validaFuncionarioMarcado(); });
    $("#btnPasso4").click(function () { return confirmaPlanejamentoAvaliacao(); });
    $("#alterarAvaliacao").click(function () { return confirmaAlteracaoAvaliacao(); });
});

function confirmaInclusaoFuncionario() {
    if (confirm("Deseja realmente realizar a inclusão do funcionário?")) {
        return true;
    }
    else {
        return false;
    }
}

function confirmaAlteracaoFuncionario() {
    if (confirm("Deseja realmente realizar a alteração do funcionário?")) {
        return true;
    }
    else {
        return false;
    }
}

function confirmaExclusaoFuncionario() {
    if (confirm("Deseja realmente realizar a exclusão do funcionário?")) {
        return true;
    }
    else {
        return false;
    }
}


function confirmaInclusaoQuestionario() {
    if (confirm("Deseja realmente realizar a inclusão do questionário?")) {
        return true;
    }
    else {
        return false;
    }
}

function confirmaAlteracaoQuestionario() {
    if (confirm("Deseja realmente realizar a alteração do questionário?")) {
        return true;
    }
    else {
        return false;
    }
}

function confirmaExclusaoQuestionario() {
    if (confirm("Deseja realmente realizar a exclusão do questionário?")) {
        return true;
    }
    else {
        return false;
    }
}


function confirmaInclusaoQuestao() {

    if (validaAlternativas()) {

        if (confirm("Deseja realmente realizar a inclusão da questão?")) {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}

function confirmaAlteracaoQuestao() {

    if (validaAlternativas()) {
        if (confirm("Deseja realmente realizar a alteração da questão?")) {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}

function confirmaExclusaoQuestao() {
    if (confirm("Deseja realmente realizar a exclusão da questão?")) {
        return true;
    }
    else {
        return false;
    }
}

function confirmaExclusao() {
    if (confirm("Deseja realmente realizar a exclusão?")) {
        return true;
    }
    else {
        return false;
    }
}

function confirmaPlanejamentoAvaliacao() {

    if (!validaFuncionarioMarcado()) {
        return false;
    }


    if (confirm("Deseja realmente confirmar o planejamento das avaliações?")) {
        return true;
    }
    else {
        return false;
    }
}

function confirmaAlteracaoAvaliacao() {
    if (confirm("Deseja realmente realizar a alteração da avaliação?")) {
        return true;
    }
    else {
        return false;
    }
}

function validaAlternativas() {
    numAlternativas = 0;
   
    if (frmCriaQuestao.txtAlternativa1.value != "") {
        numAlternativas += 1;
    }

    if (frmCriaQuestao.txtAlternativa2.value != "") {
        numAlternativas += 1;
    }

    if (frmCriaQuestao.txtAlternativa3.value != "") {
        numAlternativas += 1;
    }

    if (frmCriaQuestao.txtAlternativa4.value != "") {
        numAlternativas += 1;
    }

    if (frmCriaQuestao.txtAlternativa5.value != "") {
        numAlternativas += 1;
    }

    if (numAlternativas < 2) {
        alert("A questão deve possuir no mínimo duas alternativas");
    }

    return numAlternativas >= 2;
        
}

function marcar() {
    $('.marcar').each(
           function () {               
                $(this).prop("checked", true);               
           }
      );
}

function desmarcar() {
    $('.marcar').each(
           function () {
               $(this).prop("checked", false);
           }
      );
}

function validaFuncionarioMarcado() {
        
    var aChk = document.getElementsByClassName("marcar");

    for (var i = 0; i < aChk.length; i++) {

        if (aChk[i].checked == true) {
            return true;            
        }         
    }

    alert("Favor marcar pelo menos um funcionário.");
    return false;
}
