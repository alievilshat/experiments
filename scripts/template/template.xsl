<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('str_documenttemplate')">
          <template>
            <templateid m:left="-3" m:top="107">pk:template(document-<xsl:value-of select="id" />)</templateid>
            <templatename m:left="-141" m:top="177">
              <xsl:value-of select="templatename" />
            </templatename>
            <xsl:choose>
              <xsl:when test="systemtype=1">
                <templatetypeid m:left="-17" m:top="194">2</templatetypeid>
              </xsl:when>
              <xsl:otherwise>
                <templatetypeid m:left="108" m:top="194">1</templatetypeid>
              </xsl:otherwise>
            </xsl:choose>
            <orientation m:left="-145" m:top="209">
              <xsl:value-of select="orientation" />
            </orientation>
            <width m:left="-19" m:top="224">
              <xsl:value-of select="width" />
            </width>
            <height m:left="-147" m:top="241">
              <xsl:value-of select="height" />
            </height>
            <marginbottom m:left="-23" m:top="257">
              <xsl:value-of select="margin-bottom" />
            </marginbottom>
            <margintop m:left="-149" m:top="272">
              <xsl:value-of select="margin-top" />
            </margintop>
            <marginleft m:left="-23" m:top="288">
              <xsl:value-of select="margin-left" />
            </marginleft>
            <marginright m:left="-149" m:top="303">
              <xsl:value-of select="margin-right" />
            </marginright>
          </template>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>