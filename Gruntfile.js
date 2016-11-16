"use strict";

module.exports = function(grunt) {
  grunt.initConfig({
    pkg: grunt.file.readJSON("package.json"),
    copy: {
      main: {
        files: [
          { expand: true, cwd:'App/', src: ['js/views/**', "index.html"], dest: 'wwwroot/' }
        ],
      },
    },
    jshint: {
      all: ["*.js", "*.json", "api/**/*.js"],
      options: {
        strict: true,
        globalstrict: true,
        node: true
      }
    },
    sass: {
      dist: {
        files: {
          'wwwroot/styles/main.css': 'App/sass/main.scss'
        }
      }
    },
    uglify: {
      my_target: {
        files: {
          'wwwroot/js/main.min.js': ['App/js/**/*.js']
        }
      }
    }
  });

  grunt.registerTask('default', ['copy', 'sass', 'uglify']);
  grunt.registerTask("lint", ["jshint"]);
  grunt.loadNpmTasks('grunt-sass');
  grunt.loadNpmTasks('grunt-contrib-jshint');
  grunt.loadNpmTasks('grunt-contrib-uglify');
  grunt.loadNpmTasks('grunt-contrib-copy');
};
