#!/bin/bash
#
# Usage: build.sh [outputdirectory]

####################################################
# INIT                                             #
####################################################
BASEDIR=$(dirname "$1")
OUTPUTDIR="$1"

####################################################
# CLEAN                                            #
####################################################
echo "Cleaning previous build..."
rm "$OUTPUTDIR/cgi-bin/Addins/sForm/data/" -r

####################################################
# CGI-BIN                                          #
####################################################
echo "Building 'cgi-bin'..."
for file in cgi-bin/*; do
    cp -rv $file "$OUTPUTDIR/cgi-bin/"
done

####################################################
# SETUP                                            #
####################################################
echo "Setting up build structur..."
mkdir "$OUTPUTDIR/cgi-bin/Addins/sForm/data/html/js/"

####################################################
# JAVASCRIPT                                       #
####################################################
echo "Building 'javascript'..."
jsbuilder javascript.jsb "$OUTPUTDIR/cgi-bin/Addins/sForm/data/html/js/"

