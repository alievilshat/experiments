﻿<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('sys_language')">
          <language>
            <languageid m:left="-2" m:top="168">
              <xsl:value-of select="id" />
            </languageid>
            <languagename m:left="-2" m:top="200">
              <xsl:value-of select="name" />
            </languagename>
            <abbriviation m:left="-2" m:top="232">
              <xsl:value-of select="abbriviation" />
            </abbriviation>
            <isdefault m:left="-2" m:top="264">
              <xsl:value-of select="isdefault" />
            </isdefault>
            <cultureinfoname m:left="-2" m:top="296">
              <xsl:value-of select="cultureinfoname" />
            </cultureinfoname>
          </language>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>