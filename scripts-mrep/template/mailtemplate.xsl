<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('sys_mailtemplate')">
          <template>
            <templateid m:left="-105" m:top="161">pk:template(mail-<xsl:value-of select="id" />)</templateid>
            <templatename m:left="19" m:top="178">
              <xsl:value-of select="templatename" />
            </templatename>
            <templatetypeid m:left="-105" m:top="193">3</templatetypeid>
          </template>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>