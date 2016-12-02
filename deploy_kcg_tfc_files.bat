rem *** replace "exampleKhartaName" with actual widget name, and change the app guid and widget guid required ***
rem *** xcopy /Y "C:\APAN_HPP\git_telligent_community\kharta-coria-graphica\te.extension.kharta\Resources\Widgets\exampleKhartaName.xml" "C:\Program Files\Telligent\9.1\filestorage\defaultwidgets\{app guid}\"
rem *** xcopy /Y "C:\APAN_HPP\git_telligent_community\kharta-coria-graphica\te.extension.kharta\Resources\Widgets\exampleKhartaName\supplementary\*.* "C:\Program Files\Telligent\9.1\filestorage\defaultwidgets\{app guid}\{widget guid}"
rem *** C:\APAN_HPP\git_telligent_community\kharta-coria-graphica\te.extension.coria\Resources\Widgets\CoriaMapBookManPanel\Supplementary
rem *** C:\Program Files\Telligent\9.2\filestorage\defaultwidgets\11d248584e704d4f9bb7de3e627bc200\99f22a55f1f445848a76dd0a64452d6b\

rem **** xcopy /Y "C:\APAN_HPP\git_telligent_community\kharta-coria-graphica\te.extension.coria\Resources\Widgets\CoriaMapBookManPanel\CoriaMapBookManPanel.xml" "C:\Program Files\Telligent\9.2\filestorage\defaultwidgets\11d248584e704d4f9bb7de3e627bc200\"
rem **** xcopy /Y "C:\APAN_HPP\git_telligent_community\kharta-coria-graphica\te.extension.coria\Resources\Widgets\CoriaMapBookManPanel\Supplementary\*.*" "C:\Program Files\Telligent\9.2\filestorage\defaultwidgets\11d248584e704d4f9bb7de3e627bc200\99f22a55f1f445848a76dd0a64452d6b\"
SET tfcCss=C:\APAN_HPP\git_telligent_community\kharta-coria-graphica\te.extension.coria\Resources\Widgets\CoriaTimeline\Supplementary\CoriaTimeline.css
SET tfc=C:\APAN_HPP\git_telligent_community\kharta-coria-graphica\te.extension.coria\Resources\Widgets\CoriaTimeline\Supplementary\CoriaTimeFilter.js
SET tfcDel=C:\Program Files\Telligent\9.2\filestorage\defaultwidgets\11d248584e704d4f9bb7de3e627bc200\1937ef2b73c94cd194fdfc59626285ad\CoriaTimeFilter.js
SET loc=C:\Program Files\Telligent\9.2\filestorage\defaultwidgets\11d248584e704d4f9bb7de3e627bc200\1937ef2b73c94cd194fdfc59626285ad\
SET css=C:\Program Files\Telligent\9.2\filestorage\defaultwidgets\11d248584e704d4f9bb7de3e627bc200\1937ef2b73c94cd194fdfc59626285ad\CoriaTimeline.css
DEL "%tfcDel%"
DEL "%css%"
xcopy /Y "%tfc%" "%loc%"
xcopy /Y "%tfcCss%" "%loc%"