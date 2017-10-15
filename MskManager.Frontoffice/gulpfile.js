var gulp = require("gulp");
var source = require('vinyl-source-stream');
var request = require('request');

var ts = require("gulp-typescript");
var tsProject = ts.createProject("tsconfig.json");

gulp.task("tscompile", function () {
  return tsProject.src()
      .pipe(tsProject())
      .js.pipe(gulp.dest("built"));
});

gulp.task("copy", function () {
  var requirejs = request('https://cdnjs.cloudflare.com/ajax/libs/require.js/2.3.5/require.js')
    .pipe(source("requirejs.js"))
    .pipe(gulp.dest("built/libs"));

  var knockout = request('https://cdnjs.cloudflare.com/ajax/libs/knockout/3.4.2/knockout-min.js')
    .pipe(source("knockout.js"))
    .pipe(gulp.dest("built/libs"));
});

gulp.task("default", ["tscompile", "copy"])