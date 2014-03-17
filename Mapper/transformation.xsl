<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="urn:my-scripts" xmlns:db="http://www.naiton.com/dbaccess">
  <xsl:output method="xml" indent="yes" />
  <xsl:template match="/">
    <root>
      <xsl:for-each select="dbquery">
        <record>
          <ID>
              #<xsl:value-of select="id" />
          </ID>
          <product_code>
            <xsl:value-of select="manufacturercode" />-<xsl:value-of select="id" />
          </product_code>
          <family_code>
            <xsl:value-of select="manufacturercode" />
          </family_code>
          <version_code>
            0
          </version_code>
          <brand>
            <xsl:value-of select="brandname" />
          </brand>
          <productURL>
            <xsl:value-of select="name" />
          </productURL>
          <imageURL>
            <xsl:value-of select="imageid" />
          </imageURL>
          <name>
            <xsl:value-of select="name" />
          </name>
        </record>
      </xsl:for-each>
    </root>
  </xsl:template>
</xsl:stylesheet>