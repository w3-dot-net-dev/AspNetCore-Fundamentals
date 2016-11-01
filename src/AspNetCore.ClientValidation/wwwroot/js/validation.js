$(function() {

    var $btnValid = $(':button'),
        form = document.forms[0],
        $form = $(form),
        validator,
        validatorOptions,
        $edtEmail = $('#Email');

    function btnValid_click(event)
    {
        console.log('click');
    }

    validatorOptions = {
        //submitHandler: function(form, event) {
        //},

        invalidHandler: function(event, v) {
            console.log('in invalid');
        }
    };

    //$form.data('unobtrusiveValidation')
    //$form.data('unobtrusiveValidation').options
    validator = $form.validate();
    validator.form();

    $btnValid.on('click', btnValid_click);
});