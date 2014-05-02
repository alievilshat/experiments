<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:dubaiprint="http://www.navitas.nl/2014/Mapper/dbaccess/dubaiprint" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="dubaiprint:table('template')">
          <template>
            <templateid m:left="57" m:top="161">
              <xsl:value-of select="templateid" />
            </templateid>
            <templatename m:left="-68" m:top="175">
              <xsl:value-of select="templatename" />
            </templatename>
            <templatetypeid m:left="54" m:top="192">
              <xsl:value-of select="templatetypeid" />
            </templatetypeid>
            <orientation m:left="-70" m:top="205">
              <xsl:value-of select="orientation" />
            </orientation>
            <width m:left="52" m:top="223">
              <xsl:value-of select="width" />
            </width>
            <height m:left="-72" m:top="240">
              <xsl:value-of select="height" />
            </height>
            <marginbottom m:left="53" m:top="256">
              <xsl:value-of select="marginbottom" />
            </marginbottom>
            <margintop m:left="-71" m:top="271">
              <xsl:value-of select="margintop" />
            </margintop>
            <marginleft m:left="51" m:top="287">
              <xsl:value-of select="marginleft" />
            </marginleft>
            <marginright m:left="-73" m:top="303">
              <xsl:value-of select="marginright" />
            </marginright>
            <externalkey m:left="50" m:top="319">
              <xsl:value-of select="externalkey" />
            </externalkey>
          </template>
        </xsl:for-each>
        <xsl:for-each select="dubaiprint:table('templatedetails')">
          <templatedetails>
            <templatedetailsid m:left="51" m:top="353">
              <xsl:value-of select="templatedetailsid" />
            </templatedetailsid>
            <businessid m:left="-74" m:top="367">
              <xsl:value-of select="businessid" />
            </businessid>
            <templateid m:left="52" m:top="385">
              <xsl:value-of select="templateid" />
            </templateid>
            <templatesubject m:left="-72" m:top="401">
              <xsl:value-of select="templatesubject" />
            </templatesubject>
            <templatebody m:left="49" m:top="417">
              <xsl:value-of select="templatebody" />
            </templatebody>
            <languageid m:left="-74" m:top="433">
              <xsl:value-of select="languageid" />
            </languageid>
            <senderemail m:left="48" m:top="449">
              <xsl:value-of select="senderemail" />
            </senderemail>
            <employeeid m:left="-78" m:top="465">2</employeeid>
            <datechanged m:left="49" m:top="482">
              <xsl:value-of select="datechanged" />
            </datechanged>
          </templatedetails>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>