// JQuery
//change color for all p and div tags

$('div,p').css('background-color', 'blueviolet');

//display the html for all the div tags
$('div').each(function () {
    alert($(this).html());
});

//change color for all sections having myform class
$('.myform').css('background-color', 'GrayText');
$('.myform').css('border', '1px solid black');

//change the backgroud color of all text and password type input boxes
var inputs = $('input[type="text"],input[type = "password"]');
inputs.css('background-color', 'yellowgreen');

// selects all inputs and dispaly their values using index

var coll = $(':input'); alert($(coll[0]).val());

// selects all inputs and display each values 
$(':input').each(function () { alert($(this).val()); });

// sets all input values to foo 
$(':input').each(function () { alert($(this).val('foo')); });
//
$('form :input').each(function () { alert($(this).val()); });

$('#myform :input').each(function () { alert($(this).val()); });
$('div:contains("Mihret")').css('background-color', 'blue');

//
var output = $('#sample');
$('li').each(function (index) {
    output.html(output.html() + "<br/>" + index + " " + $(this).text());

});

$(document).ready(function () {
    // var output = $('#sample');
    var html = '';
    $('li').each(function (index) {
        html += "<br/>" + index + " " + $(this).text();

    });
    var output = $('#sample');
    output.html(html);
});

var html = '';
$('li').each(function (index, element) {
    html += "<br/>" + index + " " + $(element).text();
});
var output = $('#sample');
output.html(html);


$('li').each(function (index, element) { this.title = "some title"; });

$('li').each(function (index, element) { $(this).attr('title', 'some title'); });

$('li').attr(
    {
        title: 'some title',
        style: 'font-size: 14pt;' +
            ' background-color:gray' +
            ';color:black'
    }
);


$('li')
    .attr(
        {
            title: 'some title'
        })
    .css('font-size', ' 14pt')
    .css('background-color', 'gray')
    .css('color', 'black');
});
// Wrapping a tag 
$(document).ready(function () {
    $('span.Foo').wrapAll('<div class="BlueDiv"> Wrapping the snap tag div </div>');
});