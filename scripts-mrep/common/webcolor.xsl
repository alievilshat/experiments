<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:table('sys_colors')">
          <webcolor>
            <id m:left="-126" m:top="166" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="id" />
            </id>
            <name m:left="4" m:top="194" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="name" />
            </name>
            <hex m:left="-123" m:top="218" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="hex" />
            </hex>
            <sequenceid m:left="0" m:top="239" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="sequenceid" />
            </sequenceid>
          </webcolor>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>