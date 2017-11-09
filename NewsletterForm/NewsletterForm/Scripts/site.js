(function( $ ) {
  // constants
    var SHOW_CLASS = 'show',
        HIDE_CLASS = 'hide',
        EMPTY_CLASS = 'empty';

  // show / hide the textbox depending on "Other" selection
  $( '#Source' ).on( 'change', function (e) {

    // constants
    var SOURCE_OTHER_VALUE = '2'
    var SOURCE_ID = '#Source';
    var SOURCE_OTHER_ID = '#source-other-item';
    var isSourceOther =
        ( $( SOURCE_ID ).val() === SOURCE_OTHER_VALUE);
    var isEmpty =
        ( $( SOURCE_ID ).val() === '');    

    if ( isSourceOther ) {
        $( SOURCE_OTHER_ID )
            .addClass( SHOW_CLASS )
            .removeClass( HIDE_CLASS );
    }
    else {
        $( SOURCE_OTHER_ID )
            .removeClass( SHOW_CLASS )
            .addClass( HIDE_CLASS );

        if ( isEmpty ) {
            $( SOURCE_ID ).addClass( EMPTY_CLASS );
        }
        else {
            $( SOURCE_ID ).removeClass( EMPTY_CLASS );
        }
    }
  });

  // hide alert when user submits the form
  $( '.form-action input[type=submit]' ).click( function () {
      $( '.alert' ).remove();
  });
})( jQuery );